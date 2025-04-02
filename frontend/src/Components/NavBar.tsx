import { Link } from "react-router-dom";

const NavBar = () => {
  return (
    <nav style={{ padding: "10px", background: "#f0f0f0" }}>
      <Link to="/" style={{ marginRight: "10px" }}>
        Home Page
      </Link>
      <Link to="/admin">Admin Panel</Link>
    </nav>
  );
};

export default NavBar;
