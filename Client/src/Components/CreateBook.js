import React, { useState } from "react";
import BootstrapSwitchButton from "bootstrap-switch-button-react";
import axios from "axios";
import { useNavigate, } from "react-router-dom";
import validator from 'validator'



const CreateBook = () => {
  const [Title, setTitle] = useState("");
  const [Description, setDescription] = useState("");
  const [IsRead, setIsRead] = useState(false);
  const [Rate, setRate] = useState(0);
  const [CoverUrl, setCoverUrl] = useState("");
  const [PublisherId, setPublisherId] = useState();
  const [AuthorIds, setAuthorIds] = useState();
  const [Genre, setGenre] = useState("");
  const [toggle, setToggle] = useState(false);
  const [titleTextValidation, settitleTextValidation] = useState("");
  const [titleTextColor, settitleTextColor] = useState(false);
  const [descriptionTextValidation, setdescriptionTextValidation] = useState("");
  const [descriptionTextColor, setdescriptionTextColor] = useState(false);
  const [rateNumValidation, setrateNumValidation] = useState("");
  const [rateNumColor, setrateNumColor] = useState(false);
  const [UrlErrorMessage, setUrlErrorMessage] = useState('')
  const [urlValidateToggle, seturlValidateToggle] = useState(false);


  const navigate = useNavigate();
  const user = JSON.parse(localStorage.getItem("user"));


  const titleValidation = (e) =>{
    setTitle(e.target.value)
  
    if(Title.length <= 1 || Title.length > 40)
    {
      settitleTextColor(false)
      settitleTextValidation("Title length must be atleast 2 character and less than 40 character");
    }
    else{
      settitleTextColor(true)
      settitleTextValidation("Looks good");
    }
  }
  const descriptionValidation = (e) =>{
    setDescription(e.target.value)  
    if(Description.length <= 20 || Description.length > 100)
    {
      setdescriptionTextColor(false)
      setdescriptionTextValidation("descriptionText length must be atleast 20 characters and less than 100 characters");
    }
    else{
      setdescriptionTextColor(true)
      setdescriptionTextValidation("Looks good");
    }
  }
  
  const UrlValidation = (value) => {
    
    if (validator.isURL(value)) {
      setCoverUrl(value)
      seturlValidateToggle(true)
      setUrlErrorMessage('Is Valid URL')
    } else {
      seturlValidateToggle(false)
      setUrlErrorMessage('Is Not Valid URL')
    }
  }

  const rateValidation = (e) =>{
console.log(Rate)
    if(e.target.value >= 1 && e.target.value <= 5)
    {
      setRate(e.target.value)
      setrateNumColor(true)
      setrateNumValidation("Looks good");
    }
    else{
     
      setrateNumColor(false)
      setrateNumValidation("Rating can be between 1 to 5");
    }
  }

  const submit = async (e) => {
    e.preventDefault();
    try {
      axios
      .post("/api/Books/add-book-with-author", {
      Title,
      Description,
      IsRead,
      Rate,
      Genre,
      CoverUrl,
      PublisherId,
      AuthorIds
    }, {
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
      <h3>Add book</h3>
      <div className="form-group">
        <label>Title</label>
        <input
          type="text"
          className="form-control"
          placeholder="Enter book Title"
          required
          onChange={(e) =>titleValidation(e) }
        />
          {!titleTextColor ? <span style={{
          fontWeight: 'bold',
          color: "red",
        }}>{titleTextValidation}</span>:<span style={{
          fontWeight: 'bold',
          color: "green",
        }}>{titleTextValidation}</span>  }
      </div>


      <div className="form-group">
        <label>Description</label>
        <input
          type="text"
          className="form-control"
          placeholder="Enter email"
          required
          onChange={(e) => descriptionValidation(e)}
        />
         {!descriptionTextColor ? <span style={{
          fontWeight: 'bold',
          color: "red",
        }}>{descriptionTextValidation}</span>:<span style={{
          fontWeight: 'bold',
          color: "green",
        }}>{descriptionTextValidation}</span>  }
      </div>
      <div className="form-group m-2">
        <label>Is Read?</label>

        <BootstrapSwitchButton
          checked={false}
          onlabel="Yes"
          onstyle="danger"
          offlabel="False"
          offstyle="success"
          style="w-50 mx-2"
          onChange={() => {
            setToggle(!toggle);
          }}
        />
      </div>

      <div className="form-group">
        <label>Rate</label>
        <input
          type="number"
          className="form-control"
          placeholder="Enter Rate"
          required
          onChange={(e) =>  rateValidation(e)}
        />
          {!rateNumColor ? <span style={{
          fontWeight: 'bold',
          color: "red",
        }}>{rateNumValidation}</span>:<span style={{
          fontWeight: 'bold',
          color: "green",
        }}>{rateNumValidation}</span>  }
      </div>
      <div className="form-group">
        <label>Genre</label>
        <input
          type="text"
          className="form-control"
          placeholder="Enter genre"
          required
          onChange={(e) => setGenre(e.target.value)}
        />
      </div>

      <div className="form-group">
        <label>Cover URL</label>
        <input
          type="text"
          className="form-control"
          placeholder="Enter CoverUrl"
          required
          onChange={(e) => UrlValidation(e.target.value)}
        />
         {!urlValidateToggle ? <span style={{
          fontWeight: 'bold',
          color: "red",
        }}>{UrlErrorMessage}</span>:<span style={{
          fontWeight: 'bold',
          color: "green",
        }}>{UrlErrorMessage}</span>  }
      </div>
      <div className="form-group">
        <label>PublisherId</label>
        <input
          type="text"
          className="form-control"
          placeholder="Enter published id"
          required
          onChange={(e) => setPublisherId(e.target.value)}
        />
      </div>
      <div className="form-group">
        <label>Author Id</label>
        <input
          type="text"
          className="form-control"
          placeholder="Enter author id"
          required
          onChange={(e) => setAuthorIds([e.target.value])}
        />
      </div>


      <button type="submit" className="btn btn-primary btn-block m-3">
        Submit
      </button>
    </form>
  );
};

export default CreateBook;
