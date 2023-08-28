using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZubarskaOrdinacija.API.Middlewares;
using ZubarskaOrdinacija.Domain.Apstrakcije;
using ZubarskaOrdinacija.Domain.Apstrakcije.Baza;
using ZubarskaOrdinacija.Domain.Servisi.Izvestaj;
using ZubarskaOrdinacija.Domain.Servisi.Korisnik;
using ZubarskaOrdinacija.Domain.Servisi.Pacijent;
using ZubarskaOrdinacija.Domain.Servisi.Termini;
using ZubarskaOrdinacija.Domain.Servisi.Zubar;
using ZubarskaOrdinacija.Infrastructure.Identity;
using ZubarskaOrdinacija.Infrastructure.Identity.Entiteti;
using ZubarskaOrdinacija.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AplikacioniDbContext>(
    opts => opts.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("ZubarskaOrdinacijaDb")));

builder.Services.AddDbContext<IdentityDbContext>(
    opts => opts.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("IdentityDb")));


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
});

CookieBuilder cookie = new CookieBuilder
{
    SameSite = SameSiteMode.None,
    SecurePolicy = CookieSecurePolicy.Always,
    HttpOnly = true,
    IsEssential = true,
    Name = IdentityConstants.ApplicationScheme,
};

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = (context) =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };

    options.Events.OnSignedIn = (context) =>
    {
        return Task.CompletedTask;
    };

    options.Events.OnSigningOut = (context) =>
    {
        return Task.CompletedTask;
    };

    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;

    options.Cookie = cookie;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});


builder.Services.AddScoped<IAplikacioniDbContext>(sp => sp.GetRequiredService<AplikacioniDbContext>());
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<KorisnikService>();
builder.Services.AddScoped<TerminService>();
builder.Services.AddScoped<ZubarService>();
builder.Services.AddScoped<PacijentService>();
builder.Services.AddScoped<IzvestajService>();

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

bool isDevelopment = false;


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>(isDevelopment);

app.UseCors("MyAllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
