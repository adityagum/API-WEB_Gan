using Microsoft.EntityFrameworkCore;
using Web_API.Models;
using Web_API.Utility;

namespace Web_API.Contexts;

public class BookingManagementDbContext : DbContext
{
    public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options)
    {

    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<University> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Role>().HasData(
            new Role
            {
                Guid = Guid.Parse("0f1b24f1-b904-4c7a-2dd7-08db60bf525a"),
                Name = nameof(RoleLevel.User),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Role
            {
                Guid = Guid.Parse("0f1b24f1-b904-4856-2dd7-08db60bf525a"),
                Name = nameof(RoleLevel.Manager),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Role
            {
                Guid = Guid.Parse("0f1b24f1-b904-4933-2dd7-08db60bf525a"),
                Name = nameof(RoleLevel.Admin),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            }
        );

        builder.Entity<Employee>().HasIndex(e => new
        {
            e.Nik,
            e.Email,
            e.PhoneNumber
        }).IsUnique();

        //Relasi one to many antara education dan university
        builder.Entity<Education>()
            .HasOne(u => u.University)
            .WithMany(e => e.Educations)
            .HasForeignKey(e => e.UniversityGuid);

        /*
         * builder.Entity<University>()
         * .HasMany( e => e.Educations)
         * .WithOne( u => u.University)
         * .HasForeignKey( e => e.UniversityGuid)
         
         */

        //Relasi one to one antara Education and Employee
        builder.Entity<Education>()
            .HasOne(e => e.Employee)
            .WithOne(ed => ed.Education)
            .HasForeignKey<Education>(ed => ed.Guid);

        //Relasi antara Employee dan Account (1 to 1)
        builder.Entity<Account>()
            .HasOne(e => e.Employee)
            .WithOne(a => a.Account)
            .HasForeignKey<Account>(e => e.Guid);

        //Relasi antara rooms dan bookings (1 to many)
        builder.Entity<Booking>()
            .HasOne(r => r.Room)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.RoomGuid);

        builder.Entity<AccountRole>()
            .HasOne(r => r.Role)
            .WithMany(ac => ac.AccountRoles)
            .HasForeignKey(r => r.RoleGuid);

        builder.Entity<AccountRole>()
            .HasOne(a => a.Account)
            .WithMany(a => a.AccountRoles)
            .HasForeignKey(a => a.AccountGuid);

        //Relasi antara employee dan bookings (1 to many)
        builder.Entity<Booking>()
            .HasOne(e => e.Employee)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.EmployeeGuid);
    }

}
