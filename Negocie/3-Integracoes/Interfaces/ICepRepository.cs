using Negocie._2_Domain.Entities;

namespace Negocie._3_Integracoes.Interfaces
{
    public interface ICepRepository
    {
        Task<Endereco> ReadDadosCep(string cep);
        Task<string> InserirDadosCep(Endereco cep);
        Task<string> CriarTabelaDadosCep();
    }
}
