import axios from "axios";

const AuthorBook = (fullname
    ) => {
    let AuthorData = JSON.stringify({
        fullname
  })
  const user = JSON.parse(localStorage.getItem("user"));
    return axios
      .post("/api/Authors/add-author", AuthorData, {
        baseURL: '/',
        headers: {
            'Content-Type': 'application/json',
            Authorization: 'Bearer ' + user.token 
        }
      })
      .then((response) => {
        return response.data;
      });
    };
    const AuthorService = {
        AuthorBook,
      };
      
      export default AuthorService;