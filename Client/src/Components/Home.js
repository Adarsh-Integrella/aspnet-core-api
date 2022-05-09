import React, { useEffect, useState } from "react";
import bookService from "../services/BooksServices";

const Home = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    bookService.getAllBooks().then(
        (response) => {
          setData(response.data);
        },
        (error) => {
          console.log(error);
        }
      );
    }, []);

  return (
    <div className="App">
      <h1>Books</h1>
      <ul>
        {data &&
          data.map(({ id, title, description }) => (
            <li key={id}>
              <h3>{title}</h3>
              <p>{description}</p>
            </li>
          ))}
      </ul>
    </div>
  );
};

export default Home;
