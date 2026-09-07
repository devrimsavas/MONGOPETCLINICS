# Mongo Pet Clinic

A full-stack pet clinic management system with an ASP.NET Core Minimal API backend backed by MongoDB Atlas, and a React + TypeScript admin frontend.

## 🚀 Features

- **Pet Records** — full CRUD on pet profiles, including species, breed, age, color, and photo URL
- **Owner Information** — each pet is linked to an owner with contact details
- **Vaccination Tracking** — list all vaccinated pets, with vaccine name and administration date
- **Medical History** — store visit reason, treatment, and prescription entries per pet
- **Search & Filter** — find pets by owner name or species
- **Admin Panel** — React frontend with a searchable pet table and navigation between Home and Admin views
- **Swagger/OpenAPI** — interactive API documentation with XML comments

## 🛠 Tech Stack

### Backend
- ASP.NET Core 9 **Minimal API**
- **MongoDB** (Atlas cloud-hosted) via the official MongoDB.Bson driver
- CORS enabled (AllowAll policy) for development
- Swashbuckle (Swagger UI) with XML documentation

### Frontend
- **React 19** + **TypeScript**
- **Vite** for build tooling
- **react-router-dom** for navigation (Home / Admin routes)

## 📂 Project Structure

```
MONGOPETCLINICS/
├── PetClinic1/              # ASP.NET Core backend
│   ├── Models/               # Pet, Owner, Contact, VaccinationRecord, MedicalHistoryEntry
│   ├── Services/             # MongoDbService (data access layer)
│   └── Program.cs            # Minimal API endpoint definitions
└── frontend/                 # React + TypeScript admin UI
    └── src/Components/       # HomePage, AdminPage, PetTable, SearchBar, NavBar, Button
```

## 📖 API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/` | Health check |
| GET | `/pets` | Get all pets |
| POST | `/pets` | Add a new pet |
| PUT | `/pets` | Update a pet by name + owner |
| DELETE | `/pets` | Delete a pet by name + owner |
| GET | `/pets/by-owner/{ownerName}` | Get all pets for a specific owner |
| GET | `/pets/by-species/{species}` | Get all pets of a given species |
| GET | `/pets/vaccinated` | Get all vaccinated pets |

Full interactive documentation is available via Swagger UI when running the backend in development mode.

## ▶️ Getting Started

### Prerequisites
- .NET 9 SDK
- Node.js (for the frontend)
- A MongoDB Atlas cluster (or local MongoDB instance)

### Backend Setup

1. Navigate to the backend folder:
   ```bash
   cd PetClinic1
   ```
2. Copy `appsettings-sample.txt` to `appsettings.json` and fill in your MongoDB connection string:
   ```json
   "MongoDB": {
     "ConnectionString": "<your-atlas-connection>",
     "DatabaseName": "pethospital1"
   }
   ```
3. Run the API:
   ```bash
   dotnet run
   ```
4. Open Swagger UI at the URL shown in the console (typically `http://localhost:5120/swagger`)

### Frontend Setup

1. Navigate to the frontend folder:
   ```bash
   cd frontend
   npm install
   npm run dev
   ```
2. The app runs on Vite's default port and connects to the backend at `http://localhost:5120`

## 📝 Notes

This is a learning project built to practice ASP.NET Core Minimal APIs, MongoDB integration with a document-based data model (nested owner, vaccination, and medical history records), and connecting a React/TypeScript frontend to a .NET backend.
