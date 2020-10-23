using System;
using System.IO;
using System.Threading.Tasks;
using Confluent.Kafka;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace Vendas_Ingressos
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Carregando configurações...");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            var configuration = builder.Build();

            var logger = new LoggerConfiguration()
               .WriteTo.Console()
               .CreateLogger();
            Console.WriteLine("Configurações carregadas com sucesso...");


            logger.Information("Testando o envio de mensagens com Kafka");

            string bootstrapServer = configuration["Kafka_Broker"]; ;
            string topic = configuration["Kafka_Topic"];

            logger.Information($"BootstrapServer = {bootstrapServer}");
            logger.Information($"Topic = {topic}");

            try
            {
                ProducerConfig config = new ProducerConfig
                {
                    BootstrapServers = bootstrapServer
                };

                using var producer = new ProducerBuilder<Null, string>(config).Build();
                
                    for (int i = 0; i < int.Parse(configuration["TotalVendas"]); i++)
                    {
                        var result = await producer.ProduceAsync(
                            topic,
                            new Message<Null, string>
                            { Value = $"venda {i}" });

                        logger.Information(
                            $"Mensagem: {$"venda {i}"} | " +
                            $"Status: { result.Status}");
                    }
                logger.Information("Concluído o envio de mensagens");

            }
            catch (Exception ex)
            {
                logger.Error($"Exceção: {ex.GetType().FullName} | " +
                             $"Mensagem: {ex.Message}");
            }
        }


    }
}
