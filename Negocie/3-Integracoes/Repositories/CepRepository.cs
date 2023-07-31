using Dapper;
using Negocie._2_Domain.Entities;
using Negocie._2_Domain.Resources;
using Negocie._3_Integracoes.Interfaces;
using System.Data;

namespace Negocie._3_Integracoes.Repositories
{
    public class CepRepository : ICepRepository
    {
        private readonly IDbConnection _connection;

        public CepRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<string> InserirDadosCep(Endereco cep)
        {
            try
            {
                string insert = @"INSERT INTO Cep_Cleiton VALUES (@Cep, @Logradouro, @Complemento, @Bairro, @Localidade, @Uf, @Ibge, @Gia, @Ddd, @Siafi)";
                var teste = _connection.Execute(insert, new
                {
                    cep.Cep,
                    cep.Logradouro,
                    cep.Complemento,
                    cep.Bairro,
                    cep.Localidade,
                    cep.Uf,
                    cep.Ibge,
                    cep.Gia,
                    cep.Ddd,
                    cep.Siafi
                });

                if (teste != null)
                    return (RecourcesCep.insertOk);

                return RecourcesCep.insertError;
            }
            catch (Exception erro)
            {

                Console.WriteLine(erro);
            }
            return null;
        }

        public async Task<Endereco> ReadDadosCep(string cep)
        {
            try
            {
                if (cep != null)
                {
                    if (cep.Length == RecourcesCep.qtdDigCep && !cep.Contains("-"))
                        cep = cep.Insert(5, "-");

                    string query = "SELECT * FROM Cep_Cleiton WHERE Cep = @Cep";
                    var responseQuery = _connection.QueryFirstOrDefault<Endereco>(query, new { Cep = cep });

                    if (responseQuery != null)
                        return (responseQuery);
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro);
            }

            return null;
        }

        public async Task<string> CriarTabelaDadosCep()
        {
            
            try
            {
                var guid = new Guid();

                string createTable = @"CREATE TABLE Cep_Cleiton (
                    Cep VARCHAR(10) NOT NULL,
                    Logradouro VARCHAR(200) NOT NULL,
                    Complemento VARCHAR(100) NOT NULL,
                    Bairro VARCHAR(100) NOT NULL,
                    Localidade VARCHAR(100) NOT NULL,
                    Uf VARCHAR(2) NOT NULL,
                    Ibge VARCHAR(10) NOT NULL,
                    Gia VARCHAR(10) NOT NULL,
                    Ddd VARCHAR(5),
                    Siafi VARCHAR(10),
                    PRIMARY KEY (Cep))";

                var retornoConnection = _connection.Execute(createTable);

                if (retornoConnection != null)
                    return (RecourcesCep.tabelaCriada);

                return (RecourcesCep.tabelaNaoCriada);
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro);
            }

            return null;
        }
    }
}
