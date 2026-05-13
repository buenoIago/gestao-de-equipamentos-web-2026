// APS .NET core
// Montar um  servidor web

// Builder de um servidor web
WebApplicationBuilder builder =  WebApplication.CreateBuilder(args);

// MVC tipo de aplicação web, como vamos apresentar as informações para o usuário
builder.Services.AddControllersWithViews();

// Criação da instância do servidor web
WebApplication app = builder.Build();

// Middlewares - funções que executam em cada chamada que o nosso servidor vai receber
app.UseRouting();
app.MapDefaultControllerRoute();

// Inicia o loop da aplicação
app.Run();

