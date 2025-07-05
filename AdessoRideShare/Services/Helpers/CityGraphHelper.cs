using AdessoRideShare.Models;

namespace AdessoRideShare.Services.Helpers
{
    public static class CityGraphHelper
    {
        /// <summary>
        /// Belirli bir şehir için komşu şehirleri bulur.
        /// Komşuluk kriteri: 2.5 birimden daha kısa mesafedeki şehirler.
        /// </summary>
        public static List<City> GetAdjacentCities(City current, List<City> allCities)
        {
            return allCities
                .Where(c =>
                    c.Id != current.Id &&
                    GetDistance(current, c) <= 2.5
                )
                .ToList();
        }

        /// <summary>
        /// İki şehir arasındaki Öklidyen (Euclidean) uzaklığı hesaplar.
        /// </summary>
        private static double GetDistance(City a, City b)
        {
            int dx = a.GridX - b.GridX;
            int dy = a.GridY - b.GridY;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}