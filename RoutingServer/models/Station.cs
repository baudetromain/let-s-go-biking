namespace RoutingServer.models;

public class Station
{
    public int number { get; set; }
    public string contract_name { get; set; }
    public string name { get; set; }
    public string address { get; set; }
    public Position position { get; set; }
    public bool banking { get; set; }
    public bool bonus { get; set; }
    public int bike_stands { get; set; }
    public int available_bike_stands { get; set; }
    public int available_bikes { get; set; }
    public string status { get; set; }
    public long last_update { get; set; }

    public double distance_to(double lat, double lng)
    {
        return Math.Sqrt(Math.Pow(this.position.lat - lat, 2) + Math.Pow(this.position.lng - lng, 2));
    }
}