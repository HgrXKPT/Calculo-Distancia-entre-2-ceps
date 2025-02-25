# DistanciaCep

Este projeto é uma aplicação de console em C# que calcula a distância entre dois CEPs no Brasil utilizando a API ViaCEP e a API do Nominatim.

## Sumário

- [Introdução](#introdução)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Instalação](#instalação)
- [Uso](#uso)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Introdução

O projeto DistanciaCep permite calcular a distância entre dois CEPs brasileiros. Ele utiliza a API ViaCEP para obter os dados dos CEPs e a API Nominatim para converter os endereços em coordenadas geográficas. Em seguida, a distância é calculada utilizando a fórmula de Haversine.

## Tecnologias Utilizadas

- C#
- .NET
- HttpClient
- Newtonsoft.Json
- API ViaCEP
- API Nominatim

## Instalação

1. Clone este repositório:

    ```bash
    git clone https://github.com/seu-usuario/DistanciaCep.git
    ```

2. Abra o projeto no seu editor de código favorito.

3. Certifique-se de ter o .NET SDK instalado em sua máquina.

4. Instale as dependências do NuGet:

    ```bash
    dotnet add package Newtonsoft.Json
    ```

5. Configure a chave da API de distância no arquivo `Program.cs`:

    ```csharp
    string ApiKey = "sua_api_key_aqui";
    ```

## Uso

Execute o projeto e siga as instruções no console para calcular a distância entre dois CEPs.

```bash
dotnet run
