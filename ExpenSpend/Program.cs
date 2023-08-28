using ExpenSpend.Domain.Context;
using ExpenSpend.Domain.Models;
using ExpenSpend.Repository.Account;
using ExpenSpend.Repository.Account.Login;
using ExpenSpend.Repository.Account.Register;
using ExpenSpend.Repository.User;
using ExpenSpend.Util.Models;
using ExpenSpend.Util.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

var configuration = builder.Configuration;

builder.Services.AddDbContext<ExpenSpendDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppDbContext")?? throw new InvalidOperationException("Connection string 'AppDbContext' not found."));
});

// Configure Identity
builder.Services.AddIdentity<User, IdentityRole>(options => options
    .SignIn.RequireConfirmedEmail = true)
    .AddEntityFrameworkStores<ExpenSpendDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(2));

// Configure Email Service
builder.Services.AddSingleton(configuration.GetSection("EmailConfig").Get<EmailConfiguration>());

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IRegistorRepository, RegisterRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
