using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DigitalStore.Web;
using Supabase;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);


var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:AnonKey"];

if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseKey))
{
    throw new Exception("Supabase URL или Key не настроены в appsettings.json");
}


var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true 
};

// Регистрация Supabase Client как singleton (рекомендуется для WASM)
builder.Services.AddSingleton(sp => new Client(supabaseUrl, supabaseKey, options));

// HttpClient для Blazor WASM
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();