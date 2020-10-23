using System;
using System.Threading;
using Confluent.Kafka;
using Serilog;

namespace Processador_Vendas
{
    class Program
    {
        static void Main(string[] args)
        {



            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            logger.Information("Testando o consumo de mensagens com Kafka");

            string bootstrapServer = "localhost:9092";
            string topic = "teste";

            logger.Information($"Servidor = {bootstrapServer}");
            logger.Information($"Tópico = {topic}");

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
                using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
                {
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
            }
            catch (Exception ex)
            {
                logger.Error($"Exceção: {ex.GetType().FullName} | " +
                             $"Mensagem: {ex.Message}");
            }
        }
    }
}