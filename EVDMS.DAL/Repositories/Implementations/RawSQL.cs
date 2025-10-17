using EVDMS.DAL.Database;
using EVDMS.DAL.Repositories.Abstractions;

namespace EVDMS.DAL.Repositories.Implementations;

public class RawSQL : IRawSQL
{
    private readonly string _connectionString;
    private readonly ApplicationDbContext _context;

    public RawSQL(ApplicationDbContext context)
    {
        _context = context;
        _connectionString =
            "Server=localhost,1433;Database=EVDMS.Database;User Id=sa;Password=Strong@123;TrustServerCertificate=True;";
    }
}