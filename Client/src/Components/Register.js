import React, { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import AuthService from "../services/AuthService";
import validator from 'validator'



  const Register = () => {
    const [Username, setUsername] = useState("");
    const [Email, setEmail] = useState("");
    const [Password, setPassword] = useState("");
    const [ConfirmPassword, setConfirmPassword] = useState("");
    const [toggle, setToggle] = useState(false);
    const [errorMessage, setErrorMessage] = useState('')
    const [usernameValidation, setusernameValidation] = useState("");
    const [usernameColor, setusernameColor] = useState(false);

    const navigate = useNavigate();

    const [emailError, setEmailError] = useState('')

    const handleSignup = async (e) => {
      e.preventDefault();
      try {
        await AuthService.register(Username, Email, Password, ConfirmPassword).then(
          (response) => {
            // check for token and user already exists with 200
              console.log("Sign up successfully", response);
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

  const validateEmail = (e) => {
    var email = e.target.value
    if (validator.isEmail(email)) {
      setEmail(email)
      setToggle(true)
      return true;
      
    } else {
      setEmailError('Enter valid Email!')
      setToggle(false)

      return false;
    }
  }

const validate = (value) =>
{

  if (validator.isStrongPassword(value, {
    minLength: 8, minLowercase: 1,
    minUppercase: 1, minNumbers: 1, minSymbols: 1
  })) {
    setPassword(value);
    console.log("Password", Password)
    setToggle(true);
    return true;
  } else {
    setToggle(false);
    return false;
  }
}
const userValidation = (e) =>{
  setUsername(e.target.value)

  if(Username.length <= 1 || Username.length > 40)
  {
    setusernameColor(false)
    setusernameValidation("Username length must be atleast 2 character and less than 40 character");
  }
  else{
    setusernameColor(true)
    setusernameValidation("Looks good");
  }
}
  const ConfirmPasswordValidate = (e) =>
  {
    setConfirmPassword(e.target.value);
  }
    
  return (
    <form onSubmit={handleSignup}>
      <h3>Register</h3>
      <div className="form-group">
        <label>Username</label>
        <input
          type="text"
          className="form-control"
          placeholder="Enter username"
          required
          onChange={(e) => userValidation(e) }
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
        <label>Email address</label>
        <input
          type="email"
          className="form-control"
          placeholder="Enter email"
          required
          onChange={(e) => validateEmail(e)? setEmailError('Valid Email :')
          : setEmailError('Enter valid Email!')
        }
        />
        {!toggle?<span style={{
          fontWeight: 'bold',
          color: "red",
        }}>{emailError}</span>:<span style={{
          fontWeight: 'bold',
          color: "green",
        }}>{emailError}</span>}
      </div>


      <div className="form-group">
        <label>Password</label>
        <input
          type="password"
          className="form-control"
          placeholder="Enter password"
          required
          onChange={(e) => validate(e.target.value)?  (setErrorMessage('Is Strong Password')):setErrorMessage('Password must contain minimum 8 characters, 1 symbol, 1 uppercase and minimum 1 number')}
        />
         {!toggle?<span style={{
          fontWeight: 'bold',
          color: "red",
        }}>{errorMessage}</span>:<span style={{
          fontWeight: 'bold',
          color: "green",
        }}>{errorMessage}</span>}
      </div>
      <div className="form-group">
        <label>Password</label>
        <input
          type="password"
          className="form-control"
          placeholder="Confirm password"
          required
          onChange={(e) => ConfirmPasswordValidate(e)}
          
        />
        
        {ConfirmPassword != "" ? Password == ConfirmPassword ? <span style={{
          fontWeight: 'bold',
          color: "green",
        }}>{"Match"}</span>:<span style={{
          fontWeight: 'bold',
          color: "red",
        }}>{"Unmatch"}</span> : <span style={{
          fontWeight: 'bold',
          color: "red",
        }}>{""}</span>}
      </div>
      <button type="submit" className="btn btn-primary btn-block">
        Sign Up
      </button>
      {/* <p className="forgot-password text-right">
        Already have an account?
        <Link className="forgot-password text-right" to={"/login"}>
          Login
        </Link>
      </p> */}
    </form>
  );
};

export default Register