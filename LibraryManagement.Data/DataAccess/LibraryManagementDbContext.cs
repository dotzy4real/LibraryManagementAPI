using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Data.Entity;

namespace LibraryManagement.Data.DataAccess
{
    public class LibraryManagementDbContext : DbContext, IDbContext
    {
        private string? _connectionString;
        public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options) : base(options)
        {
        }

        public LibraryManagementDbContext()
        {
        }

        public LibraryManagementDbContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.SetConnectionString(connectionString);
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<BookReservation> BookReservations { get; set; }
        public virtual DbSet<AvailableBookNotification> AvailableBookNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("book_pkey");
                entity.ToTable("Book");
                entity.Property(e => e.Id)
                    .UseIdentityColumn()
                    .HasColumnName("Id");

                entity.Property(e => e.Title)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("Title");

                entity.Property(e => e.Author)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("Author");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("Description");

                entity.Property(e => e.IsAvailable)
                    .HasColumnType("bit")
                    .HasColumnName("IsAvailable");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("customer_pkey");
                entity.ToTable("Customer");
                entity.Property(e => e.Id)
                    .UseIdentityColumn()
                    .HasColumnName("Id");

                entity.Property(e => e.FirstName)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("FirstName");

                entity.Property(e => e.LastName)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("LastName");

                entity.Property(e => e.Address)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("Address");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("PhoneNumber");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("Email");
            });

            modelBuilder.Entity<BookReservation>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("bookreservation_pkey");
                entity.ToTable("BookReservation");
                entity.Property(e => e.Id)
                    .UseIdentityColumn()
                    .HasColumnName("Id");

                entity.HasOne(b => b.Book).WithMany(d => d.BookReservations)
                    .HasForeignKey(b => b.BookId)
                    .HasConstraintName("fk_book_bookreservations").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Customer).WithMany(c => c.BookReservations)
                    .HasForeignKey(b => b.CustomerId)
                    .HasConstraintName("fk_customer_bookreservations").OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("StartDate");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("EndDate");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ReturnDate");

                entity.Property(e => e.ReservedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ReservedDate");

                entity.Property(e => e.ReservationStatus)
                    .HasColumnType("bit")
                    .HasColumnName("ReservationStatus");

                entity.Property(e => e.RequestAvailabilityNotification)
                    .HasColumnType("bit")
                    .HasColumnName("RequestAvailabilityNotification");
            });

            modelBuilder.Entity<AvailableBookNotification>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("availablebooknotification_pkey");
                entity.ToTable("AvailableBookNotification");
                entity.Property(e => e.Id)
                    .UseIdentityColumn()
                    .HasColumnName("Id");

                entity.HasOne(b => b.Book).WithMany(d => d.AvailableBookNotifications)
                    .HasForeignKey(b => b.BookId)
                    .HasConstraintName("fk_book_availablebooknotifications").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Customer).WithMany(d => d.AvailableBookNotifications)
                    .HasForeignKey(b => b.CustomerId)
                    .HasConstraintName("fk_customer_availablebooknotifications").OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.NotificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NotificationDate");

                entity.Property(e => e.IsNotificationSent)
                    .HasColumnType("bit")
                    .HasColumnName("IsNotificationSent");
            });


            modelBuilder.Entity<OAuthAuthentication>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("oauthauthentication_pkey");
                entity.ToTable("OAuthAuthentication");
                entity.Property(e => e.Id)
                    .UseIdentityColumn()
                    .HasColumnName("Id");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("Name");

                entity.Property(e => e.ClientId)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("ClientId");

                entity.Property(e => e.ClientSecret)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("ClientSecret");

                entity.Property(e => e.Scope)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("Scope");

                entity.Property(e => e.GrantType)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("GrantType");

                entity.Property(e => e.Status)
                    .HasColumnType("bit")
                    .HasColumnName("Status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public void OnModelCreatingPartial(ModelBuilder modelBuilder) { }


        public DbSet<T> GetEntities<T>() where T : class
        {
            return base.Set<T>();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("data source=.;initial catalog=LibraryManagement;User ID=sa;Password=P@ssw0rd;TrustServerCertificate=True;");

    }
}
