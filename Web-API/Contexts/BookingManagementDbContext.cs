using Microsoft.EntityFrameworkCore;
using Web_API.Models;

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
    public DbSet<Education> educations { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<University> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Employee>().HasIndex(e => new
        {
            e.Nik,
            e.Email,
            e.PhoneNumber
        }).IsUnique();

        //Relasi one to many antara education dan university
        builder.Entity<Education>()
            .HasOne(u => u.university)
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
            .HasOne(e => e.employee)
            .WithOne(e => e.education)
            .HasForeignKey<Education>(e => e.Guid);

        //Relasi one to one antara Account dan Employee
        builder.Entity<Account>()
            .HasOne(e => e.employee)
            .WithOne(a => a.account)
            .HasForeignKey<Account>(a => a.Guid);

        //Relasi one to many antara Account dan Account Role
        builder.Entity<AccountRole>()
            .HasOne(a => a.account)
            .WithMany(ar => ar.accountRoles)
            .HasForeignKey(ar => ar.RoleGuid);

        //Relasi one to many antara role dan accountroles
        builder.Entity<AccountRole>()
            .HasOne(r => r.role)
            .WithMany(ar => ar.accountRoles)
            .HasForeignKey(ar => ar.AccountGuid);
        
        //Relasi one to many antara Booking dan Employee
        builder.Entity<Booking>()
            .HasOne(e => e.Employee)
            .WithMany(b => b.bookings)
            .HasForeignKey(ar => ar.EmployeeGuid);

        //Relasi one to many antara Booking dan Room
        builder.Entity<Booking>()
            .HasOne(r => r.Room)
            .WithMany(b => b.bookings)
            .HasForeignKey(b => b.RoomGuid);
    }

}
