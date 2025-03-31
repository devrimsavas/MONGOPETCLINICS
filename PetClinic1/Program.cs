using PetClinic1.Services;
using PetClinic1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models; //optional since it is minimalist program
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//register MongoDbService
builder.Services.AddSingleton<MongoDbService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Mongo Pet Hospital",
        Description = "Pet Hospital"
    });
    //enable xml
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapGet("/", () => "Hello World!");


//add an end-point to test our database 

//get all


app.MapGet("/pets", async (MongoDbService db) =>
{
    var pets = await db.GetAllPetsAsync();
    return Results.Ok(pets);
})
.WithName("GetAllPets")
.WithSummary("Returns all pets in the system")
.WithDescription("Fetches pet records");

//add new pet
//add [frombody ] optional since it is minimal program 
app.MapPost("/pets", async ([FromBody] Pet pet, MongoDbService db) =>
{
    await db.AddPetAsync(pet);
    return Results.Created($"/pets/{pet.Id}", pet);
});




app.Run();
