using Assignment_EFC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment_EFC.Contexts
{
    internal class DataContext : DbContext
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DataStorage\DB\Assignment_EFC\Contexts\sql_db.mdf;Integrated Security=True;Connect Timeout=30";

        #region constructors
        public DataContext() 
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)      //constructor av klassen DataContext som ärver från DbContext klassen i EFC, som instansierar ifrån DbContextOptions. Initierar DbContext med de som skickas in, så vi kan interagera med databasen.
        {

        }

        #endregion


        #region overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  //config. av databasen,vilken connectionstring den skall till. anv sig av SqlServer.
        {
            if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionString);
        }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion


        #region entities

        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<AddressEntity> Addresses { get; set; } = null!;
        public DbSet<TicketEntity> Tickets { get; set; } = null!;
        public DbSet<CommentEntity> Comments { get; set; } = null!;

        #endregion
    }
}
