import React, { useState, useEffect } from "react";
import "../node_modules/bootstrap/dist/css/bootstrap.min.css";
import {Routes, Route, Link } from "react-router-dom";
import Register from "./Components/Register";
import Home from "./Components/Home";
import Login from "./Components/Login";
import AddBook from "./Components/CreateBook";
import AuthService from "./services/AuthService";
import AddAuthor from "./Components/AddAuthor";
import GetBookAuthorById from "./Components/GetBookAuthorById";
import GetPublisherBookWithAuthor from "./Components/GetPublisherBookWithAuthor";
import GetAllPublisher from "./Components/GetAllPublisher"
import DeleteAuthor from "./Components/DeleteAuthorById";
import GetAuthorById from "./Components/GetAuthorById";
import "./App.css";


function App() {
  const [currentUser, setCurrentUser] = useState(undefined);

  useEffect(() => {
    const user = AuthService.getCurrentUser();
    // console.log(user);

    if (user) {
      setCurrentUser(user);
    }
  }, []);

  const logOut = () => {
    AuthService.logout();
  };

  return (
    <div className="App">
      <nav className="navbar navbar-expand navbar-light bg-light">
        <div className="container-fluid">
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarTogglerDemo03"
            aria-controls="navbarTogglerDemo03"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <a className="navbar-brand" href="#">
          <Link to={"/"} className="nav-link">
                  BookX
                </Link>          </a>
          <div className="collapse navbar-collapse" id="navbarTogglerDemo03">
            <div className="navbar-nav me-auto mb-2 mb-lg-0">
              
              <div className="dropdown m-1">
                <button
                  className="btn btn-secondary dropdown-toggle"
                  type="button"
                  id="dropdownMenuButton"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  Books
                </button>
                <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                  <Link to={"/GetBookAuthorById"} className="dropdown-item">
                    Get book by Id
                  </Link>
                  {currentUser && (
                    <Link to={"/addbook"} className="dropdown-item">
                      Add Book
                    </Link>
                  )}
                </div>
              </div>
              <div className="dropdown m-1">
                <button
                  className="btn btn-secondary dropdown-toggle"
                  type="button"
                  id="dropdownMenuButton"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  Author
                </button>
                <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                {currentUser && (
                  <Link to={"/addauthor"} className="dropdown-item">
                    Add author
                  </Link>
              )}
                  <Link to={"/GetPublisherBookWithAuthor"} className="dropdown-item">
                    Get Publisher Book With Author
                  </Link>
                  <Link to={"/GetAuthorById"} className="dropdown-item">
                    Get Author by Id
                  </Link>
                  {currentUser && (
                  <Link to={"/DeleteAuthor"} className="dropdown-item">
                    Delete Author
                  </Link>
              )}
                </div>
              </div>

              <div className="dropdown m-1">
                <button
                  className="btn btn-secondary dropdown-toggle"
                  type="button"
                  id="dropdownMenuButton"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  Publisher
                </button>
                <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                  <Link to={"/GetAllPublisher"} className="dropdown-item">
                  Get All Publisher
                  </Link>
                </div>
              </div>
              
            </div>

            {currentUser ? (
              <div className="navbar-nav ms-auto">
                <li className="nav-item">
                  <a href="/login" className="nav-link" onClick={logOut}>
                    Logout
                  </a>
                </li>
              </div>
            ) : (
              <div className="navbar-nav ms-auto">
                <li className="nav-item">
                  <Link to={"/login"} className="nav-link">
                    Login
                  </Link>
                </li>

                <li className="nav-item">
                  <Link to={"/register"} className="nav-link">
                    Register
                  </Link>
                </li>
              </div>
            )}
          </div>
        </div>
      </nav>
      <div className="auth-wrapper">
        <div className="auth-inner">
          <Routes>
            <Route exact path="/" element={<Home />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/addbook" element={<AddBook />} />
            <Route path="/addauthor" element={<AddAuthor />} />
            <Route path="/GetBookAuthorById" element={<GetBookAuthorById />} />
            <Route
              path="/GetPublisherBookWithAuthor"
              element={<GetPublisherBookWithAuthor />}
            />
            <Route
              path="/GetAllPublisher"
              element={<GetAllPublisher />}
            />
            <Route
              path="/DeleteAuthor"
              element={<DeleteAuthor />}
            />
            <Route
              path="/GetAuthorById"
              element={<GetAuthorById />}
            />
          </Routes>
        </div>
      </div>
    </div>
  );
}

export default App;
