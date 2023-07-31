namespace Negocie._2_Domain.Models
{
    public class EnderecoModel
    {
        public EnderecoModel(string cep, string logradouro, string complemento, string bairro, string localidade,
            string uf, string ibge, string gia, string ddd, string siafi)
        {
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
            Ibge = ibge;
            Gia = gia;
            Ddd = ddd;
            Siafi = siafi;
        }

        public string Cep { get; }
        public string Logradouro { get; }
        public string Complemento { get; }
        public string Bairro { get; }
        public string Localidade { get; }
        public string Uf { get; }
        public string Ibge { get; }
        public string Gia { get; }
        public string Ddd { get; }
        public string Siafi { get; }
    }
}
