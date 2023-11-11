using AnnuaireEntrepriseAPI;
using AnnuaireEntrepriseAPI.Database;
using AnnuaireEntrepriseAPI.DTOs;
using AnnuaireEntrepriseAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Insérer les 1000 salariés

builder.Services.AddDbContext<AnnuaireEntrepriseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerContext"));
});

//var optionsBuilder = new DbContextOptionsBuilder<AnnuaireEntrepriseContext>();
//optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerContext"));

//using (var dataContext = new AnnuaireEntrepriseContext(optionsBuilder.Options))
//{
//    var data = new DataGenerator(dataContext);
//    data.GenerateData(dataContext);
//}

//Console.WriteLine("Insertion des données terminée !");
//Console.ReadLine();


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();



