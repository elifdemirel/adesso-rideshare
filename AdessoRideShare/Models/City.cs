using System;
namespace AdessoRideShare.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GridX { get; set; } // 0–19
        public int GridY { get; set; } // 0–9
    }
}

