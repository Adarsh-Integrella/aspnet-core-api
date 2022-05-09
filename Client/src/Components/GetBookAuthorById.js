import React, { useState } from "react";
import AuthorService from "../services/AuthorService";
import axios from "axios";

const GetBookAuthor = () => {
  const [data, setData] = useState([]);
  const [para, setpara] = useState(0);
  const [toggle, setToggle] = useState(false);

  const submit = (event) => {
    event.preventDefault();

    axios
      .get(`/api/Authors/get-author-with-book-by-id/${para}`, {
        baseURL: "/",
      })
      .then((res) => {
        // console.log(res);
        // const newData = JSON.stringify(res.data)
        setData(res.data);
        setToggle(true);
      });

    console.log(data);
  };

  return (
    <div className="App">
      <h1>Requested data</h1>
      <form onSubmit={submit}>
        <div className="form-group">
          <label>Enter Id to get author and book</label>
          <input
            type="number"
            className="form-control"
            placeholder="Enter Id to get author and book"
            required
            onChange={(e) => setpara(e.target.value)}
          />
        </div>
        <button type="submit" className="btn btn-primary btn-block m-3">
          Submit
        </button>
      </form>
      {toggle && (
        <ul>
          <h2>Result</h2>
          <li>
            <h4>Author Name : {data.fullName}</h4>
          </li>
          <li>
            <h4>Book written : {data.booksTitle}</h4>
          </li>
        </ul>
      )}
    </div>
  );
};

export default GetBookAuthor;
