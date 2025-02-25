using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DistanciaCep;
using static System.Net.WebRequestMethods;
using System.Globalization;

namespace DistanciaCep {
    internal class Program {

        private static readonly HttpClient client = new HttpClient();
        public static async Task Main(string[] args) {

            string ApiKey = "SUA-API-KEY";
            string cep, URL , cep2, URL2;

            Console.WriteLine("Digite um CEP para fazer a busca");
            cep = Console.ReadLine();
            URL = $"https://viacep.com.br/ws/{cep}/json/";
            

            var response = await client.GetStringAsync(URL);
            var ViaCepResponse = JsonConvert.DeserializeObject<ViaCepResponse>(response);

            Console.WriteLine("Digite o segundo CEP para fazer a busca");
            cep2 = Console.ReadLine();
            URL2 = $"https://viacep.com.br/ws/{cep2}/json/";

            var response2 = await client.GetStringAsync(URL2);
            var ViaCepResponse2 = JsonConvert.DeserializeObject<ViaCepResponse>(response2);


            Console.WriteLine(ViaCepResponse.Bairro);
            Console.WriteLine(ViaCepResponse.Logradouro);

            Console.WriteLine(ViaCepResponse2.Bairro);
            Console.WriteLine(ViaCepResponse2.Logradouro);

            string DistanceAPI = $"https://www.distance.to/api?origins={cep}&destinations={cep2}&key={ApiKey}";

            var (latitude, Longitude) = await Cordenadas.GetCoordinates(cep);
            var (latitude2, Longitude2) = await Cordenadas.GetCoordinates(cep2);

            Console.WriteLine($"Teste: lat: {latitude} lon: {Longitude}");
            Console.WriteLine($"Teste: lat: {latitude2} lon: {Longitude2}");

            (double lat1, double lon1) = (double.Parse(latitude,CultureInfo.InvariantCulture), double.Parse(Longitude, CultureInfo.InvariantCulture));
            (double lat2, double lon2) = (double.Parse(latitude2, CultureInfo.InvariantCulture), double.Parse(Longitude2, CultureInfo.InvariantCulture));



            double distancia = CalcularDistancia(lat1,lon1,lat2,lon2);
            Console.WriteLine($"Distancia entre o CEP-A  e o CEP-B é {distancia:F2}KMs");

        }

        static double CalcularDistancia(double latitude, double longitude, double latitude2, double longitude2){
            //raio da terra
            double R = 6371;

            // Converter as coordenadas de graus para radianos
            double deltaLat = (latitude2 - latitude) * (Math.PI / 180);
            double deltaLong = (longitude2 - longitude) * (Math.PI / 180);

            double lat1Rad = latitude * (Math.PI / 180);
            double lat2Rad = latitude2 * (Math.PI / 180);

            // Fórmula haversine
            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(deltaLong / 2) * Math.Sin(deltaLong / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a),Math.Sqrt(1 - a));

            double d = R * c;

            return d;


        }
    }
}
