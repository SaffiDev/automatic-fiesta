using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DigitalStore.Web;
using DigitalStore.Web.Services;
using Supabase;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:Key"];

if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseKey))
{
    throw new Exception("Supabase URL или Key не настроены в appsettings.json");
}

builder.Services.AddBlazoredLocalStorage();  // регистрирует async + sync

// Supabase клиент с persistence
builder.Services.AddSingleton(provider =>
{
    // Создаём временный scope, чтобы взять scoped ISyncLocalStorageService
    using var scope = provider.CreateScope();
    var syncLocalStorage = scope.ServiceProvider.GetRequiredService<ISyncLocalStorageService>();

    var persistor = new BlazoredSessionPersistence(syncLocalStorage);

    var options = new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true,
        SessionHandler = persistor  // или SessionPersistor, если ругается — попробуй оба
    };

    var client = new Client(supabaseUrl, supabaseKey, options);
    return client;
});

builder.Services.AddScoped<SupabaseService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<WishlistService>(); 
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();