using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Proxy.models;

namespace Proxy
{
    internal class Proxy
    {
        private static HttpListener httpListener = new HttpListener();
        private static HttpClient httpClient = new HttpClient();
        private static string api_key = "e0beee5c2f1a0468494223125c4c35b1761aaffc";
        private static List<Station> stations = new List<Station>();
        
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Initiating the proxy...");
            
            Console.Write("Retrieving existing stations... ");
            try
            {
                await retrieveStations();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nAn error occured while trying to retrieve the list of existing stations.");
                System.Environment.Exit(1);
            }
        }

        private static async Task retrieveStations()
        {
            Stopwatch clock = Stopwatch.StartNew();
            
            HttpResponseMessage response = await httpClient.GetAsync($"https://api.jcdecaux.com/vls/v1/stations?apiKey={api_key}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            stations = JsonSerializer.Deserialize<List<Station>>(responseBody);
            
            clock.Stop();
            Console.WriteLine($"Retrieved {stations.Count} stations in {clock.Elapsed.Milliseconds}ms !");
        }
    }
}