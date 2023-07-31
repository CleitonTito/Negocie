using Microsoft.AspNetCore.Mvc;
using Negocie._2_Domain.Entities;
using Negocie._2_Domain.Models;
using Negocie._3_Integracoes.Interfaces;

namespace Negocie._5_Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CepController : ControllerBase
    {
        private readonly IViaCepIntegracao _viaCepIntegracao;
        private readonly ICepRepository _cepRepository;
        public CepController(IViaCepIntegracao viaCepIntegracao, ICepRepository cepRepository)
        {
            _viaCepIntegracao = viaCepIntegracao;
            _cepRepository = cepRepository;
        }

        [HttpPost("{cep}")]
        [ProducesResponseType(typeof(Endereco), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Endereco>> InsertCepRepository(string cep) 
        {
            var retornoBuscaCep = await _viaCepIntegracao
                .ObterDadosViaCep(cep);

            if (retornoBuscaCep != null) 
            {
                var response = await _cepRepository
                    .InserirDadosCep(retornoBuscaCep);

                if (response != null)
                    return Ok(response);
            }

            return NotFound();
        }

        [HttpGet("{cep}")]
        [ProducesResponseType(typeof(EnderecoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<EnderecoModel>> GetRepository(string cep)
        {
            var cepData = _cepRepository.ReadDadosCep(cep);

            if (cepData.Result == null)
                return NotFound();

            return Ok(cepData.Result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<string>> CreateCepRepository()
        {
            var retornoCriarTabelaCep = await _cepRepository.CriarTabelaDadosCep();

            if (retornoCriarTabelaCep == null)
                return NotFound();

            return Ok(retornoCriarTabelaCep);
        }
    }
}
