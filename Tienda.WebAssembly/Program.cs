using Tienda.WebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Blazored.LocalStorage;
using Blazored.Toast;

using Tienda.WebAssembly.Servicios.Desarrollo;
using Tienda.WebAssembly.Servicios.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Tienda.WebAssembly.Extensiones;

using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using IgniteUI.Blazor.Controls;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddIgniteUIBlazor(typeof(IgbIconModule));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5019/api/") });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddBlazoredToast();

builder.Services.AddScoped<IServicioUsuario,ServicioUsuario>();

builder.Services.AddScoped<IServicioCategoria, ServicioCategoria>();

builder.Services.AddScoped<IServicioProducto, ServicioProducto>();

builder.Services.AddScoped<IServicioCarrito, ServicioCarrito>();

builder.Services.AddScoped<IServicioPedido, ServicioPedido>();

builder.Services.AddSweetAlert2();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExt>();

await builder.Build().RunAsync();
