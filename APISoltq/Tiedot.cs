namespace APISoltq
{
    public class Sahko  
    {
        public DateTimeOffset timestamp  { get; set; }
        public string reportingGroup  { get; set; }
        public string locationName { get; set; }
        public double value { get; set; }
        public string unit { get; set; }

    }
    
    public class Data
    {
        public DateTimeOffset timestamp { get; set; }
        public double value { get; set; }
        public string unit { get; set; }
    }
}