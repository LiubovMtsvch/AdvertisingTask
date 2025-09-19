namespace AdvertisingTask.Models
{
    public class LocationNode
    {
        public string Name { get; set; } = ""; 
        public List<string> Platforms { get; set; } = new(); 
        public Dictionary<string, LocationNode> Children { get; set; } = new(); 
    }

}
