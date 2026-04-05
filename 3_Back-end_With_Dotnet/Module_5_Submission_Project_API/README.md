# Simple User API

A minimal ASP.NET Core Web API for managing users, using an in-memory mock database.

## Requirements

- .NET 8.0 or later

## Running the Project

```bash
dotnet run
```

The API will start on `http://localhost:5167` by default.

---

## Authentication

All endpoints except `GET /` require the following header:

```
Authorization: secret-token
```

Requests with a missing or invalid token will receive a `401 Unauthorized` response.

---

## Endpoints

### Health Check

```
GET /
```

Returns a plain text confirmation that the API is running. No authentication required.

---

### Get All Users

```
GET /api/User/all
Authorization: secret-token
```

Returns a list of all users.

---

### Get User by ID

```
GET /api/User/{id}
Authorization: secret-token
```

Returns a single user by their numeric ID. Returns `404` if not found.

---

### Add User

```
POST /api/User/{name}/{email}
Authorization: secret-token
```

Creates a new user with the given name and email.

**Validation rules:**
- Name must not be empty or whitespace
- Email must contain `@`
- Email must be unique

Returns `400` if validation fails, `409` if the email is already in use.

---

### Update User Name

```
PUT /api/User/{id}/{newName}
Authorization: secret-token
```

Updates the name of the user with the given ID. Returns `404` if the user does not exist.

---

### Delete User

```
DELETE /api/User/{id}
Authorization: secret-token
```

Removes the user with the given ID. Returns `404` if the user does not exist.

---

## Data Model

```
User
├── Id     (int)
├── Name   (string)
└── Email  (string)
```

---

## Middleware

The app uses three custom middleware layers, applied in this order:

1. **Error handler** — catches unhandled exceptions and returns a `500` JSON response
2. **Auth guard** — validates the `Authorization` header on all routes except `/`
3. **Request logger** — logs the HTTP method, path, and response status code to the console

HTTP logging (full request/response bodies up to 4 KB) is also enabled via the built-in ASP.NET `HttpLogging` middleware.

---

## Mock Database

`DbControllerMock` is a singleton in-memory store pre-seeded with 10 users (Alice through Jack). Data does not persist between application restarts.

---

## Notes

- Email validation is minimal — a value like `a@` will pass the current check
- This project is intended for learning/demo purposes and is not production-ready
