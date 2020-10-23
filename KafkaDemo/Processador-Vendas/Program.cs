using System;
using System.Threading;
using Confluent.Kafka;
using Serilog;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Processador_Vendas
{
    class Program
    {
        static void Main(string[] args)
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

            logger.Information("Testando o consumo de mensagens com Kafka");

            string bootstrapServer = configuration["Kafka_Broker"]; ;
            string topic = configuration["Kafka_Topic"];

            logger.Information($"BootstrapServer = {bootstrapServer}");
            logger.Information($"Topic = {topic}");

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServer,
                GroupId = $"{topic}-group-0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                
                    consumer.Subscribe(topic);

                    try
                    {
                        while (true)
                        {
                            var cr = consumer.Consume(cts.Token);
                            logger.Information(
                                $"Mensagem lida: {cr.Message.Value}");
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumer.Close();
                        logger.Warning("Cancelada a execução do Consumer...");
                    }
                
            }
            catch (Exception ex)
            {
                logger.Error($"Exceção: {ex.GetType().FullName} | " +
                             $"Mensagem: {ex.Message}");
            }
        }
    }
}