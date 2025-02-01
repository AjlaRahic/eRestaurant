using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;
using WebApplication4.Data;
using WebApplication4.Helper;
using WebApplication4.Helper.ErrorHandler;
using WebApplication4.Service;
using WebApplication4.SignalR;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Očisti default log provajdere i dodaj console logovanje
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Dodaj servise u kontejner.
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("db1")));

// Konfiguracija Swaggera
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AutorizacijaSwaggerHeader>();
});

// Konfiguracija Email servisa
builder.Services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, MailerService>();
//Konfiguracija Log Servisa
builder.Services.AddScoped<LogService>();
// Dodavanje CORS-a
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Dodaj CORS origin (tvoj Angular port)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


var app = builder.Build();

// Postavite UseRouting() prije UseEndpoints()
app.UseRouting();
app.UseCors("AllowLocalhost");
// Mapirajte SignalR hub
app.MapHub<PorukeHub>("/poruke-hub-putanja");
// Konfiguracija HTTP zahtjeva
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();

//// Postavite CORS pravila
//app.UseCors(options =>
//    options.SetIsOriginAllowed(x => true)
//    .AllowAnyMethod()
//    .AllowAnyHeader()
//    .AllowCredentials());

// Postavite autentifikaciju i autorizaciju **između** UseRouting() i UseEndpoints()
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();  // Omogućava serviranje statičkih fajlova

app.MapControllers();

app.Run();
