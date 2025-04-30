# ASP.NET Core Minimal API - Login Handler

This project is a minimal ASP.NET Core Web API that processes login requests via HTTP `POST`. It accepts `email` and `password` from the **form-urlencoded** request body (as used in Postman) and returns appropriate HTTP status codes and responses.

---

## 🚀 Requirements

- .NET 8 or higher SDK
- A tool like Postman or `curl` to test requests

---

## 📌 Functionality

### ✅ POST `/`

Accepts `email` and `password` via `application/x-www-form-urlencoded` body.

- If `email = admin@example.com` and `password = admin1234` → `200 OK` with:


