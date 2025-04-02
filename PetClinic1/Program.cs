using PetClinic1.Services;
using PetClinic1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models; //optional since it is minimalist program
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//anti-corst
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", options =>
    {
        options.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});
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

//use cors
app.UseCors("AllowAll");

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

//delete a pet with name and owner name using REQUEST 
app.MapDelete("/pets", async ([FromBody] DeleteByNameOwnerRequest request, MongoDbService db) =>
{

    try
    {
        var result = await db.DeletePetByNameAndOwnerAsync(request);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);

    }

});

//update by name and owner name 
app.MapPut("/pets", async ([FromBody] Pet updatedPet, MongoDbService db) =>
{
    try
    {
        var result = await db.UpdatePetByNameAndOwnerAsync(updatedPet.Name, updatedPet.Owner.Name, updatedPet);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

//owner's all pet 
app.MapGet("/pets/by-owner/{ownerName}", async (string ownerName, MongoDbService db) =>
{
    try
    {
        var pets = await db.GetPetsByOwnerNameAsync(ownerName);
        if (pets.Count == 0)
            return Results.NotFound($"No pets found for owner '{ownerName}'");

        return Results.Ok(pets);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

//get species 
app.MapGet("/pets/by-species/{species}", async (string species, MongoDbService db) =>
{
    try
    {
        var pets = await db.GetPetsBySpeciesAsync(species);
        if (pets.Count == 0)
            return Results.NotFound($"No pets found for species '{species}'");

        return Results.Ok(pets);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/pets/vaccinated", async (MongoDbService db) =>
{
    try
    {
        var pets = await db.GetVaccinatedPetsAsync();
        if (pets.Count == 0)
            return Results.NotFound("No vaccinated pets found.");

        return Results.Ok(pets);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});






app.Run();
