import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

const DeleteAuthor = () => {
  const [para, setpara] = useState();
  const navigate = useNavigate();

  const submit = (event) => {
    const user = JSON.parse(localStorage.getItem("user"));
    event.preventDefault();
    axios
      .delete(`/api/Authors/Delete-author-by-id/${para}`, {
        baseURL: "/",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + user.token,
        },
      })
      .then(() => {
        navigate("/");
        window.location.reload();
      });
  };

  return (
    <div className="App">
      <h1>Delete Author</h1>
      <form onSubmit={submit}>
        <div className="form-group">
          <label>Enter id to delete author</label>
          <input
            type="number"
            className="form-control"
            placeholder="Enter Id"
            required
            onChange={(e) => setpara(e.target.value)}
          />
        </div>
        <button type="submit" className="btn btn-primary btn-block m-3">
          Submit
        </button>
      </form>
    </div>
  );
};

export default DeleteAuthor;
