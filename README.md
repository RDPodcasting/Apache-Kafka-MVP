# Projeto de venda de ingressos ![CodeQL](https://github.com/RDPodcasting/Apache-Kafka-MVP/workflows/CodeQL/badge.svg)
> Um sistema de venda de ingressos dividido em duas partes, onde uma fique gerando as vendas de ingressos aos montes, como se fosse um horário de pico das vendas para o show de um grande artista. 
> E para que o sistema não caia, teremos um processador dessas vendas, utilizando mensageria com apache kafka de forma escalavel para aguentar a alta demanda de ingressos vendidos.

## Tecnologias utilizadas :rocket:

| Nome   | Descrição                  |
| ---------- |  --------------------- |
| Apache Kafka  | Consumo de mensagens de um tópico |
| Console Application | Criada com .NET Core 3.1    |
| Serilog   |  Gerenciamento de logs da aplicação   |

## Índice :pencil:

* [Instalação](#instalação)
* [Como usar](#como-usar)
* [Demo](#apache-kafka-mvp)

## Instalação
> Realize a instalação e, faça as configurações necessárias para que suba o servidor `zookeeper` e o `broker kafka` na porta padrão `localhost:9092`.

| Nome   | Descrição                    | Obrigátorio               |
| ---------- | ------------------------------ | --------------------- |
| 🌎[Java 8](https://www.oracle.com/java/technologies/javase/javase-jdk8-downloads.html)       |     Requisito necessário para o kafka ser instalado            |:white_check_mark: |
| 🌎[Apache Kafka](https://kafka.apache.org/downloads)        |     Streaming de menssagens        |      :white_check_mark:     |
| 🌎[Conduktor](https://www.conduktor.io/download/)   |        Gerenciador com interface para o Kafka       | :x: |

## Como usar
> Após realizar as configurações do `kafka` e já com o `servidor` em pé crie um tópico com o nome que você irá configurar logo depois na demo.

### Criar tópico no apache kafka
```
kafka-topics --bootstrap-server localhost:9092 --topic <nome_topico> --create
```
### Listar tópicos
```
kafka-topics --bootstrap-server localhost:9092 --list
```

## Apache-Kafka-MVP 
> No arquivo `appsettings.json`, você deve colocar as configurações que você fez nos passos anteriores.
```
{
  "Kafka_Broker": "localhost:9092",
  "Kafka_Topic": "<nome_topico>",
  "TotalVendas": "10"
}
```
### 1. Simulador de vendas:
> Console application `Vendas-Ingressos`.

### 2. Processador de vendas:
> Console application `Processador-Vendas`.
