
# 🔐 ASP.NET Core Minimal API - Login Application

This is an ASP.NET Core application demonstrating handling of login logic using both **custom middleware** and **route-based endpoints**. It accepts email and password via `POST` requests and responds with appropriate HTTP status and messages.

---

## 🚀 Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Any API testing tool like **Postman**, **curl**, or browser for `GET` requests

---

## 📂 Project Structure

- `CustomLoginMiddleware` → Handles `POST /` and performs validation using form-urlencoded data
- `POST /login` → Also accepts `email` and `password` via query string and handles authentication

---

## 📌 Supported Routes

### 1. POST `/` (Handled by Middleware)

- Accepts: `application/x-www-form-urlencoded` body
- Valid credentials:
  - **email**: `admin@example.com`
  - **password**: `admin1234`

#### ✅ Valid Login

**Request**
```http
POST /
Content-Type: application/x-www-form-urlencoded

email=admin@example.com&password=admin1234
```

**Response**
```
Status: 200 OK
Body: Successful login
```

#### ❌ Invalid Login

**Request**
```http
POST /
Content-Type: application/x-www-form-urlencoded

email=wrong@example.com&password=wrongpass
```

**Response**
```
Status: 400 Bad Request
Body: Invalid login
```

#### ❌ Missing Fields

**Request**
```http
POST /
Content-Type: application/x-www-form-urlencoded

email=admin@example.com
```

**Response**
```
Status: 400 Bad Request
Body: Invalid input for 'password'
```

---

### 2. POST `/login` (Handled by Endpoint)

- Accepts: `email` and `password` as **query parameters** (e.g. `/login?email=...&password=...`)

#### ✅ Valid Login

**Request**
```
POST /login?email=admin@example.com&password=admin1234
```

**Response**
```
Status: 200 OK
Body: Successful loging
```

#### ❌ Invalid or Missing Fields

**Response**
```
Status: 400 or 401
Body: Invalid input for 'email' or 'password' or Invalid login
```

---

### 3. GET `/` or any unhandled path

**Response**
```
Status: 200 OK
Body: No response!
```

---

## 🧪 Testing with `curl`

```bash
curl -X POST http://localhost:5000/ -H "Content-Type: application/x-www-form-urlencoded" -d "email=admin@example.com&password=admin1234"

curl -X POST "http://localhost:5000/login?email=admin@example.com&password=admin1234"
```

---

## 📦 How to Run

```bash
dotnet run
```

Then test using Postman, curl, or browser.
