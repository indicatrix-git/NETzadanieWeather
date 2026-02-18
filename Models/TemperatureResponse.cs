namespace NETzadanie.Models
{
    public class TemperatureResponse
    {
        public string City { get; set; } = string.Empty;
        public decimal TemperatureC { get; set; }
        public DateTime MeasuredAtUtc { get; set; }
    }
}
