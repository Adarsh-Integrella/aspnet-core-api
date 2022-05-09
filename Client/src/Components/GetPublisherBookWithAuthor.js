import React, { useState } from "react";
import AuthorService from "../services/AuthorService";
import axios from "axios";

const GetPublisherBookWithAuthor = () => {
  const [data, setData] = useState([]);
  const [para, setpara] = useState(0);
  const [toggle, setToggle] = useState(false);

  const submit = (event) => {
    event.preventDefault();

    axios
      .get(
        `/api/Publishers/get-publisher-book-with-author/${para}`
      ,
      {
        baseURL: '/',
      })
      .then((res) => {
        // console.log(res);
        // const newData = JSON.stringify(res.data)
        setData(res.data);
        setToggle(true)
      });
  };

  return (
    <div className="App">
      <h1>Requested data</h1>
      <form onSubmit={submit}>
        <div className="form-group">
          <label>Enter Id to get publisher, author and book</label>
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
      {toggle && (      <ul>
<h2>Result</h2>
        <li>
          <h4>Publisher Name : {data.name}</h4>
        </li>
        <li>
        {data.bookAuthors.map(x => 
        <div key={Math.random()}>
            <h4>Book Name : <div>{x.bookName}</div></h4>
            <h4>Book Author : <div></div>{x.bookAuthors}</h4>
            </div>
            )}
        </li>
      </ul>)}
      
    </div>
  );
};

export default GetPublisherBookWithAuthor;
