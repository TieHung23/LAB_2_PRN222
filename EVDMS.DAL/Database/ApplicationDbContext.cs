using EVDMS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DAL.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Dealer> Dealers { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<TestDrive> TestDrives { get; set; }
    public DbSet<VehicleConfig> VehicleConfigs { get; set; }
    public DbSet<VehicleModel> VehicleModels { get; set; }
}