import { useState } from "react";
import Button from "./Button";

interface SearchBarProps {
  onShowAll: () => void;
  onSearchByName: (name: string) => void;
}

const SearchBar: React.FC<SearchBarProps> = ({ onShowAll, onSearchByName }) => {
  const [searchTerm, setSearchTerm] = useState("");

  return (
    <div className="search-bar">
      <Button
        name="all-pet-button"
        caption="Get All Pets"
        callFunc={onShowAll}
        buttonClass="button-5"
      />

      <input
        type="text"
        placeholder="Search pet by name"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
        className="search-input"
      />

      <Button
        name="search-pet-button"
        caption="Search"
        callFunc={() => onSearchByName(searchTerm)}
        buttonClass="button-5"
      />
    </div>
  );
};

export default SearchBar;
