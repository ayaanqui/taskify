using System.Net;
using api.Database;
using api.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddGoogle(options =>
    {
        var section = builder.Configuration.GetSection("Authentication:Google");
        options.ClientId = section["ClientId"];
        options.ClientSecret = section["ClientSecret"];
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.CallbackPath = "/oauth/google/callback";
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

var app = builder.Build();

// Run migrations
using (var Scope = app.Services.CreateScope())
{
    Console.WriteLine("Running migrations...");

    var context = Scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    await context.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
    var response = new { exception.Message };
    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    await context.Response.WriteAsJsonAsync(response);
}));

app.MapControllers();

app.Run();
