using Microsoft.EntityFrameworkCore;
using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.Repositories;
using Web_API.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection"); //fungsinya untuk mendapatkan connection string dari JSON
builder.Services.AddDbContext<BookingManagementDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddSingleton(typeof(IMapper<,>), typeof(Mapper<,>));
builder.Services.AddTransient<IEmailService, EmailService>(_ => new EmailService(
                smtpServer: builder.Configuration["Email:SmtpServer"],
                smtpPort: int.Parse(builder.Configuration["Email:SmtpPort"]),
                fromEmailAddress: builder.Configuration["Email:FromEmailAddress"]));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
