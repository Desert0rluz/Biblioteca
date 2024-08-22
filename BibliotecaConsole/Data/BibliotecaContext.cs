using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class BibliotecaContext : DbContext
{
    DbSet<Book> Books { get; set; }
    DbSet<Owner> Owners { get; set; }
    DbSet<Friend> Friends { get; set; }

    private string connectionString = "Data Source=KLEBER;Initial Catalog=BibliotecaV1;Integrated Security=True;" +
        "Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(connectionString)
            .UseLazyLoadingProxies();
    }
}
