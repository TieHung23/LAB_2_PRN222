namespace EVDMS.Core.DTOs
{
    public class OrderViewModel
    {
        public Guid OrderId
        {
            get; set;
        }
        public DateTime OrderDate
        {
            get; set;
        }
        public string CustomerFullName
        {
            get; set;
        }
        public string CustomerEmail
        {
            get; set;
        }
        public string DealerName
        {
            get; set;
        }
        public string VehicleModelName
        {
            get; set;
        }
        public string VehicleColor
        {
            get; set;
        }
        public decimal TotalAmount
        {
            get; set;
        }
        public string Status
        {
            get; set;
        }
    }
}
