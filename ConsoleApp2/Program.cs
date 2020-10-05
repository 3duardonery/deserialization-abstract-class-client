using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient(baseUrl: "http://localhost:5000/api");
            var requisicao = new RestRequest(resource: "values", Method.POST);

            var carteira = new CarteiraDeClientes();
            carteira.CarteiraId = "1234567890";
            carteira.Clientes = new List<Pessoa>
            {
                new Juridica
                {
                    Id = 1,
                    Cnpj = "12345678901",
                    InscricaoEstadual = "123",
                    Address = "Endereço da Empresa",
                    InscricaoMunicipal = "123654",
                    Name = "Empresa Teste"
                },
                new Fisica
                {
                    Id = 2,
                    Cpf = "12345678901",
                    Rg = "123",
                    Name = "Teste",
                    Address = "Rua do Teste"
                }
            };

            var carteiraJson = JsonConvert.SerializeObject(carteira, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                SerializationBinder = new CustomJsonSerializationBinder(typeof(Pessoa).Namespace)
            });

            requisicao.AddJsonBody(carteiraJson);

            Console.WriteLine(carteiraJson);

            var resposta = client.Execute(requisicao);

            Console.WriteLine(resposta.StatusCode);
        }
    }
}
