using Negocie._2_Domain.Entities;
using Refit;

namespace Negocie._3_Integracoes.Refit
{
    public interface IViaCepIntegracaoRefit
    {
        [Get("/ws/{cep}/json")]
        Task<ApiResponse<Endereco>> ObterDadosViaCep(string cep);
    }
}
