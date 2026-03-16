/*
API CLIENT

Responsável por configurar comunicação com backend.
Aqui definimos:
- baseURL
- headers padrão
- interceptors (ex: auth token)

NUNCA chamamos fetch direto nos componentes.
*/

import axios from "axios";

export const api = axios.create({
  baseURL: "http://localhost:5101/api", // endereço da API no backend
  headers: {
    "Content-Type": "application/json",
  },
});