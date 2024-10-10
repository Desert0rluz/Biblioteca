using BibliotecaShared.Models.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace Data;

public class BibliotecaContext : DbContext
{
    DbSet<Book> Books { get; set; }
    DbSet<Owner> Owners { get; set; }
    DbSet<Friend> Friends { get; set; }
    DbSet<Gender> Genders { get; set; }
    DbSet<Loan> Loans { get; set; }

    private string connectionString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = BibliotecaV1; " +
        "Integrated Security = True; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; " +
        "Multi Subnet Failover=False";


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(connectionString)
            .UseLazyLoadingProxies();
    }
}
