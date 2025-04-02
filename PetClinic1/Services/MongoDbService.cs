

using Microsoft.AspNetCore.Authorization.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using PetClinic1.Models;



namespace PetClinic1.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Pet> _pets;

        public MongoDbService(IConfiguration config)
        {
            var connectionString = config.GetSection("MongoDB:ConnectionString").Value;
            var databaseName = config.GetSection("MongoDB:DatabaseName").Value;

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            // Use the collection for Pet documents
            _pets = database.GetCollection<Pet>("pethospitalcollection");
            //_collection = database.GetCollection<BsonDocument>("pethospitalcollection");
        }


        //find all 

        public async Task<List<Pet>> GetAllPetsAsync()
        {
            return await _pets.Find(_ => true).ToListAsync();
        }




        public async Task<Pet> GetPetByIdAsync(string id)
        {
            return await _pets.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddPetAsync(Pet pet)
        {
            await _pets.InsertOneAsync(pet);
        }

        public async Task UpdatePetAsync(string id, Pet updatedPet)
        {
            await _pets.ReplaceOneAsync(p => p.Id == id, updatedPet);
        }

        public async Task DeletePetAsync(string id)
        {
            await _pets.DeleteOneAsync(p => p.Id == id);
        }

        //delete by pet name AND owner name 
        public async Task<string> DeletePetByNameAndOwnerAsync(DeleteByNameOwnerRequest deleteRequest)
        {
            var result = await _pets.DeleteOneAsync(p =>
                p.Name == deleteRequest.Name && p.Owner.Name == deleteRequest.OwnerName);

            if (result.DeletedCount == 0)
            {
                return $"Pet with name '{deleteRequest.Name}' and owner '{deleteRequest.OwnerName}' not found.";
            }

            return $"Pet '{deleteRequest.Name}' owned by '{deleteRequest.OwnerName}' deleted successfully.";
        }

        //update by pet name AND owner name 
        public async Task<string> UpdatePetByNameAndOwnerAsync(string petName, string ownerName, Pet updatedPet)
        {
            var result = await _pets.ReplaceOneAsync(
                p => p.Name == petName && p.Owner.Name == ownerName,
                updatedPet
            );

            if (result.MatchedCount == 0)
            {
                return $"Pet with name '{petName}' and owner '{ownerName}' not found.";
            }

            return $"Pet '{petName}' owned by '{ownerName}' updated successfully.";
        }

        //find an owners all pets
        public async Task<List<Pet>> GetPetsByOwnerNameAsync(string ownerName)
        {
            return await _pets.Find(p => p.Owner.Name == ownerName).ToListAsync();
        }

        //find all pets by species 
        public async Task<List<Pet>> GetPetsBySpeciesAsync(string species)
        {
            return await _pets.Find(p => p.Species == species).ToListAsync();
        }

        //vaccinated pets 
        public async Task<List<Pet>> GetVaccinatedPetsAsync()
        {
            return await _pets.Find(p => p.Vaccinations.Any()).ToListAsync();
        }










    }

    //delete pet name and owner name request
    public class DeleteByNameOwnerRequest
    {
        public string? Name { get; set; }
        public string? OwnerName { get; set; }
    }
}