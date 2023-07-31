using Negocie._2_Domain.Entities;

namespace Negocie._3_Integracoes.Interfaces
{
    public interface IViaCepIntegracao
    {
        Task<Endereco> ObterDadosViaCep(string cep);
    }
}
