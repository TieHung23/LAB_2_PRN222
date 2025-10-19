namespace EVDMS.Core.DTOs;

public class AdminIndexResponse
{
    public int NewCustomerCount { get; set; }
    public int NewVechicleCount { get; set; }
    public int NewOrderCount { get; set; }
    public int NewDealCount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public double CustomerGrowth { get; set; }
    public double VehicleGrowth { get; set; }
    public double OrderGrowth { get; set; }
    public double DealGrowth { get; set; }
}