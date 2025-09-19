using AdvertisingTask.Models;

namespace AdvertisingTask.Services
{
    public class AdPlatformStorage
    {
        private readonly LocationNode _root = new();

        public void LoadFromLines(IEnumerable<string> lines)
        {
            _root.Children.Clear();
            _root.Platforms.Clear();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || !line.Contains(":")) 
                    continue;

                var parts = line.Split(':', 2);
                var name = parts[0].Trim();
                var locations = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var loc in locations)
                {
                    var segments = loc.Split('/', StringSplitOptions.RemoveEmptyEntries);
                    var node = _root;

                    foreach (var segment in segments)
                    {
                        if (!node.Children.TryGetValue(segment, out var child))
                        {
                            child = new LocationNode { Name = segment };
                            node.Children[segment] = child;
                        }
                        node = child;
                    }

                    node.Platforms.Add(name);
                }
            }
        }

        public List<string> Search(string location)
        {
            var result = new HashSet<string>();
            var segments = location.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var node = _root;

            foreach (var segment in segments)
            {
                result.UnionWith(node.Platforms);
                if (!node.Children.TryGetValue(segment, out node))
                    break;
            }

            if (node != null)
                result.UnionWith(node.Platforms);

            return result.ToList();
        }
    }
}
