﻿using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using RoutingServer.models;

namespace RoutingServer
{
    internal class RoutingServer
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
            
            httpListener.Prefixes.Add("http://localhost:8080/");
            httpListener.Start();

            Console.CancelKeyPress += delegate
            {
                httpListener.Stop();
                httpListener.Close();
                Environment.Exit(0);
            };

            while (true)
            {
                HttpListenerContext context = httpListener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }
                
                
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

        private static Station getClosestStation(double lat, double lng)
        {
            Station closest_station = null;
            double best_distance = -1;
            
            foreach (var station in stations)
            {
                double distance = station.distance_to(lat, lng);
                
                if (best_distance < 0 || distance < best_distance)
                {
                    closest_station = station;
                    best_distance = distance;
                }
            }

            return closest_station;
        }
    }
}