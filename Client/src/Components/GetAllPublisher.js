import React, { useEffect, useState } from "react";
import axios from "axios";

const GetAllPublisher = () => {
  const [data, setData] = useState([]);
  const [sort, setsort] = useState("");

  const getData = () => {
    axios.get(`/api/Publishers/get-all-publishers?${sort}`,{
      baseURL: '/',
    })
    .then(
        (response) => {
          setData(response.data);
        },
        (error) => {
          console.log(error);
        }
      );
    };
const handleSorting = () =>{
    sort===""?setsort("sortBy=name_desc"):setsort("")
    getData()
}

  return (
    <div className="App">
      <h1>Publishers Names</h1>
      <ul>
        {data &&
          data.map(({ id, name }) => (
            <li key={id}>
              <p>{name}</p>
            </li>
          ))}
      </ul>
      <button type="button" className="btn btn-primary btn-block" onClick={getData} >
          Get data
      </button>
      <button type="button" className="btn btn-primary btn-block m-2" onClick={handleSorting} >
          Ascending/descending 
      </button>
    </div>
  );
};

export default GetAllPublisher;
