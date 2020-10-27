dotnet sonarscanner begin /k:RDPodcasting_Apache-Kafka-MVP /d:sonar.host.url=https://sonarcloud.io /d:sonar.login=e76a60465f09cd030732e6cda7c51f296f2f2401 /o:codinsights 
dotnet restore KafkaDemo.sln
dotnet build KafkaDemo.sln
dotnet-sonarscanner end /d:sonar.login="e76a60465f09cd030732e6cda7c51f296f2f2401"