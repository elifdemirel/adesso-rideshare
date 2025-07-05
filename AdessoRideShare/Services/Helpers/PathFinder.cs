using AdessoRideShare.Models;

namespace AdessoRideShare.Services.Helpers
{
    public static class PathFinder
    {
        /// <summary>
        /// Başlangıç ve hedef şehirler arasında geçilebilecek bir rota bulur.
        /// En kısa rota BFS algoritması ile aranır.
        /// </summary>
        public static List<City> GetRoute(City from, City to, List<City> allCities)
        {
            var queue = new Queue<List<City>>();
            var visited = new HashSet<int>();
            queue.Enqueue(new List<City> { from });

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                var current = path.Last();

                if (current.Id == to.Id)
                    return path;

                visited.Add(current.Id);

                var neighbors = CityGraphHelper.GetAdjacentCities(current, allCities);
                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor.Id))
                    {
                        var newPath = new List<City>(path) { neighbor };
                        queue.Enqueue(newPath);
                    }
                }
            }

            // Ulaşılabilir bir rota yoksa boş liste döndür
            return new List<City>();
        }
    }
}