import React, { useState } from "react";
import BootstrapSwitchButton from "bootstrap-switch-button-react";
import bookService from "../services/BooksServices";
import { useNavigate, } from "react-router-dom";
import axios from "axios";



const AddAuthor = () => {
  const [Fullname, setFullname] = useState("");
  const [usernameValidation, setusernameValidation] = useState("");
  const [usernameColor, setusernameColor] = useState(false);

  const userValidation = (e) =>{
    setFullname(e.target.value)  
    if(Fullname.length <= 1 || Fullname.length > 40)
    {
      setusernameColor(false)
      setusernameValidation("Full name length must be atleast 2 character and less than 40 character");
    }
    else{
      setusernameColor(true)
      setusernameValidation("Looks good");
    }
  }

  const navigate = useNavigate();
  const user = JSON.parse(localStorage.getItem("user"));

  const submit = async (e) => {
    e.preventDefault();
    try {
      axios
      .post("/api/Authors/add-author", {Fullname}, {
        headers: {
            'Content-Type': 'application/json',
            Authorization: 'Bearer ' + user.token 
        }
      }).then(
          () => {
            navigate("/");
            window.location.reload();
          },
          (error) => {
            console.log(error);
          }
        );
      } catch (err) {
        console.log(err);
      }
  };

  return (
    <form onSubmit={submit}>
      <h3>Add Author</h3>
      <div className="form-group">
        <label>Add Author name</label>
        <input
          type="text"
          className="form-control"
          placeholder="Enter Author name"
          required
          onChange={(e) => userValidation(e)}
        />
        {!usernameColor ? <span style={{
          fontWeight: 'bold',
          color: "red",
        }}>{usernameValidation}</span>:<span style={{
          fontWeight: 'bold',
          color: "green",
        }}>{usernameValidation}</span>  }
      </div>
      <button type="submit" className="btn btn-primary btn-block m-3">
        Submit
      </button>

    </form>
  );
};

export default AddAuthor;
