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

var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true 
};

// Регистрация Supabase Client
builder.Services.AddSingleton(sp => new Client(supabaseUrl, supabaseKey, options));


builder.Services.AddScoped<SupabaseService>();
builder.Services.AddScoped<CartService>();
// HttpClient для Blazor WASM
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();