import { useState } from "react";
import SearchBar from "./SearchBar"; // Adjust path if needed

interface Owner {
  name: string;
}

interface Pet {
  id: string;
  name: string;
  owner: Owner;
}

const PetTable = () => {
  const [pets, setPets] = useState<Pet[]>([]);

  const showPets = async () => {
    const response = await fetch("http://localhost:5120/pets");
    const data = await response.json();
    setPets(data);
  };

  const searchByName = async (name: string) => {
    const response = await fetch(`http://localhost:5120/pets/by-name/${name}`);
    const data = await response.json();
    setPets(data);
  };

  return (
    <div className="pet-table-container">
      <SearchBar onShowAll={showPets} onSearchByName={searchByName} />

      <div className="table-container">
        <table className="styled-table">
          <thead>
            <tr>
              <th>Pet Unique Id</th>
              <th>Pet Id</th>
              <th>Pet Name</th>
              <th>Pet Owner</th>
            </tr>
          </thead>
          <tbody id="pet-table-body">
            {pets.map((pet, index) => (
              <tr key={index}>
                <td>{pet.id}</td>
                <td>{index}</td>
                <td>{pet.name}</td>
                <td>{pet.owner.name}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default PetTable;
