import React, { useState} from "react";
import { useNavigate } from "react-router-dom";
import AuthService from "../services/AuthService";
import validator from 'validator'

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState('')
  const [toggle, setToggle] = useState(false);
  const [usernameValidation, setusernameValidation] = useState("");
  const [usernameColor, setusernameColor] = useState(false);

  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      await AuthService.login(username, password).then(
        () => {
          navigate("/");
          window.location.reload();
        },
        (error) => {
          console.log(error);
        }
      );
    } catch (err) {
      // console.log(err);
    }
  };
  const userValidation = (e) =>{
    setUsername(e.target.value)
  
    if(username.length <= 1 || username.length > 40)
    {
      setusernameColor(false)
      setusernameValidation("Username length must be atleast 2 character and less than 40 character");
    }
    else{
      setusernameColor(true)
      setusernameValidation("Looks good");
    }
  }
  const validate = (value) => {

   

    if (validator.isStrongPassword(value, {
      minLength: 8, minLowercase: 1,
      minUppercase: 1, minNumbers: 1, minSymbols: 1
    })) {
      setPassword(value)
      setToggle(true);
      return true;
    } else {
      setToggle(false);
      return false;
    }
  };

  return (
    <form onSubmit={handleLogin}>
      <h3>Login</h3>
      <div className="form-group">
        <label>Username</label>
        <input
          type="text"
          className="form-control"
          placeholder="Enter username"
          required
          onChange={(e) =>  userValidation(e)}
        />
         {!usernameColor ? <span style={{
          fontWeight: 'bold',
          color: "red",
        }}>{usernameValidation}</span>:<span style={{
          fontWeight: 'bold',
          color: "green",
        }}>{usernameValidation}</span>  }
      </div>
      
      <div className="form-group">
        <label>Password</label>
        <input
          type="password"
          className="form-control"
          placeholder="Enter password"
          required
          onChange={(e) => validate(e.target.value)?  setErrorMessage('Is Strong Password'):setErrorMessage('Password must contain minimum 8 characters, 1 symbol, 1 uppercase and minimum 1 number')
          }
        />
        {!toggle?<span style={{
          fontWeight: 'bold',
          color: "red",
        }}>{errorMessage}</span>:<span style={{
          fontWeight: 'bold',
          color: "green",
        }}>{errorMessage}</span>}
      </div>

      <button type="submit" className="btn btn-primary btn-block">
        Submit
      </button>
    </form>
  );
};

export default Login;
