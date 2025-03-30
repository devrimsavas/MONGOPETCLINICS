using PetClinic1.Services;
using PetClinic1.Models;
using Microsoft.AspNetCore.Mvc; //optional since it is minimalist program

var builder = WebApplication.CreateBuilder(args);
//register MongoDbService
builder.Services.AddSingleton<MongoDbService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


//add an end-point to test our database 

//get all
app.MapGet("/pets", async (MongoDbService db)=> {
    var pets=await db.GetAllPetsAsync();
    return Results.Ok(pets);
});

//add new pet
//add [frombody ] optional since it is minimal program 
app.MapPost("/pets", async ([FromBody] Pet pet, MongoDbService db) =>
{
    await db.AddPetAsync(pet);
    return Results.Created($"/pets/{pet.Id}", pet);
});




app.Run();
