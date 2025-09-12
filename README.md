# Mongo Pet Hospital API

This project is a backend web application built with ASP.NET Core Minimal APIs and MongoDB.
It simulates a Pet Clinic / Hospital Management System, allowing CRUD operations on pets, their owners, vaccinations, and medical history.

## 🚀 Features

CRUD operations for pet records

Owner-based queries – fetch all pets by a specific owner

Species-based queries – list pets by species

Vaccination tracking – fetch vaccinated pets

Medical history entries for each pet

Swagger/OpenAPI 3.0 integration for API documentation

## 📂 Models

Pet → stores core pet information (name, species, vaccination records, medical history)

Owner → stores owner details (name, contact)

Contact → owner’s contact information

VaccinationRecord → vaccination type, date

MedicalHistoryEntry → previous treatments and visits

📖 API Endpoints
Method Endpoint Description
GET / Health check
GET /pets Get all pets
POST /pets Add new pet
PUT /pets Update pet by name + owner
DELETE /pets Delete pet by name + owner
GET /pets/by-owner/{owner} Get pets by owner
GET /pets/by-species/{type} Get pets by species
GET /pets/vaccinated Get all vaccinated pets

## 👉 Full documentation available at:

http://localhost:5120/swagger/index.html

⚙️ Setup & Run
Prerequisites

.NET 6 SDK

MongoDB (local or cloud, e.g. Atlas)

Run
dotnet build
dotnet run

Swagger UI will be available at:
👉 http://localhost:5120/swagger/index.html

📝 Notes

CORS enabled (AllowAll policy) for development purposes.

Data persistence handled via MongoDbService.

This is a learning project to practice ASP.NET Core Minimal APIs and MongoDB integration.
