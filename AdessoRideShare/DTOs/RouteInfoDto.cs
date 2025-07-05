namespace AdessoRideShare.DTOs
{
    public class RouteInfoDto
    {
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public double TotalDistance { get; set; }
        public bool IsDirectConnection { get; set; }
        public bool RouteExists { get; set; }
        public List<string> IntermediateCities { get; set; }
        public int TotalStops { get; set; }
    }
}
