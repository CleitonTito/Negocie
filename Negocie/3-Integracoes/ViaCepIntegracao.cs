using Negocie._2_Domain.Entities;
using Negocie._3_Integracoes.Interfaces;
using Negocie._3_Integracoes.Refit;

namespace Negocie._3_Integracoes
{
    public class ViaCepIntegracao : IViaCepIntegracao
    {
        private readonly IViaCepIntegracaoRefit _viaCepIntegracaoRefit;
        
        public ViaCepIntegracao(IViaCepIntegracaoRefit viaCepIntegracaoRefit)
        {
            _viaCepIntegracaoRefit = viaCepIntegracaoRefit;
        }

        public async Task<Endereco> ObterDadosViaCep(string cep)
        {
            try
            {
                var response = await _viaCepIntegracaoRefit.ObterDadosViaCep(cep);

                if (response != null && response.IsSuccessStatusCode)
                    return response.Content;
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro);
            }

            return null;
        }
    }
}
