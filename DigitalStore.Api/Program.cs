using Supabase;
using DigitalStore.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Supabase Client
builder.Services.AddScoped<Supabase.Client>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var url = config["Supabase:Url"] ?? throw new Exception("Supabase Url not found");
    var key = config["Supabase:Key"] ?? throw new Exception("Supabase Key not found");

    var options = new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
    };

    return new Supabase.Client(url, key, options);
});

var app = builder.Build();

app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();