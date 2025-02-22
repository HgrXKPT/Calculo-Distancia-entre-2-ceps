using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DistanciaCep;
using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using static DistanciaCep.Cordenadas;

namespace DistanciaCep {


    public class Cordenadas {

        private static readonly HttpClient client = new HttpClient();

        public static async Task<(string,string)> GetCoordinates(string cep) {

            try {
                string ViaCepURL = $"https://viacep.com.br/ws/{cep}/json/";
                var response = await client.GetStringAsync(ViaCepURL);
                var ViaCepResponse = JsonConvert.DeserializeObject<ViaCepResponse>(response);

                if(ViaCepResponse != null && !string.IsNullOrEmpty(ViaCepResponse.Logradouro)) {

                    string address = $"{ViaCepResponse.Logradouro} {ViaCepResponse.Localidade} {ViaCepResponse.Uf} Brasil";
                    string nominatimUrl = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json&addressdetails=1";

                    var (latitude, longitude) = await GetNominatimLocalAsync(nominatimUrl);

                    return (latitude, longitude);


                } else {
                    throw new Exception  ($"Impossivel calcular ");
                }
            }catch(HttpRequestException ex) {
                throw new Exception($"Error HTTP: {ex.Message}");

            }catch(Exception ex) {
                throw new Exception($"Ocorreu uma excessão: {ex.Message }");
            }
            
        }
        public static async Task<(string, string)> GetNominatimLocalAsync(string url) {

            using (var client = new HttpClient()) {

                client.DefaultRequestHeaders.Add("User-Agent","Mozilla/5.0 (compatible; AcmeInc/1.0)");

                try {
                    var response = await client.GetStringAsync(url);

                    var nomatimResponse = JsonConvert.DeserializeObject<List<NominatimResponse>>(response);

                    if (nomatimResponse != null && nomatimResponse.Count > 0 ) {

                        var result = nomatimResponse.First();
                        return (result.Lat, result.Lon);
                    } else {
                        throw new Exception($"Coordernada não encontrada");
                    }
                } catch(HttpRequestException ex) {
                    throw new Exception ($"Error HTTP: {ex}");

                }
                
                
                catch(Exception ex) {
                    throw new Exception ("Error inusitado: " + ex);
                }
                 
                
            }

        }








        public class NominatimResponse {
            public string Lat { get; set; }
            public string Lon { get; set; }
        }

        public class Address {
            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }
        }
    }
}
