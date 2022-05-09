import axios from "axios";
import authHeader from "./auth-header";

axios.defaults.baseURL = window.location.href
const getAllBooks = () => {
  return axios.get(`/api/Books/get-all-books`,{
    baseURL: '/',
  });
};


const AddBook = (title,
  description,
  isRead,
  rate,
  genre,
  coverUrl,
  publisherId,
  authorId) => {
  let BookData = JSON.stringify({
    title,
    description,
    isRead,
    rate,
    genre,
    coverUrl,
    publisherId,
    authorId
})
const host = window.location.hostname;
const user = JSON.parse(localStorage.getItem("user"));
  return axios
    .post(`/api/Books/add-book-with-author`, BookData, {
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

const bookService = {
  getAllBooks,
  AddBook,
};

export default bookService;