import Button from "./Button";
import { useState } from "react";

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
    console.log(data);
    setPets(data);
  };

  return (
    <div>
      <Button
        name="all-pet-button"
        caption="Get All Pets"
        callFunc={showPets}
        buttonClass={"button-5"}
      />

      <div className="table-container">
        <table className="styled-table">
          <thead>
            <tr>
              <th>Pet Unique Id</th>
              <th> Pet Id </th>
              <th>Pet Name</th>
              <th>Pet Owner</th>
            </tr>
          </thead>
          <tbody id="pet-table-body">
            {pets.map((pet, index) => (
              <tr key={index}>
                <td>{pet.id}</td>
                <td>{index} </td>
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
