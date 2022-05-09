import axios from "axios";

const register = async (username, email, password, confirmPassword) => {
  let registerData = JSON.stringify({
    username,
      email,
      password,
      confirmPassword
})

  return axios
    .post(`/api/Authentication/register`, registerData, {
      baseURL: '/',
      headers: {
          'Content-Type': 'application/json',
      }
    })
    .then((response) => {
      if (response.data.token) {
        localStorage.setItem("user", JSON.stringify(response.data));
      }

      return response.data;
    });
  };




const login = async (username, password) => {
  let data = JSON.stringify({
    password,
    username
})

  return axios
    .post("/api/Authentication/login", data, {
      baseURL: '/',
      headers: {
          'Content-Type': 'application/json',
      }
    })
    .then((response) => {
      if (response.data.token) {
        localStorage.setItem("user", JSON.stringify(response.data));
      }

      return response.data;
    });
};

const logout = () => {
  localStorage.removeItem("user");
};

const getCurrentUser = () => {
  return JSON.parse(localStorage.getItem("user"));
};

const authService = {
  register,
  login,
  logout,
  getCurrentUser,
};

export default authService;