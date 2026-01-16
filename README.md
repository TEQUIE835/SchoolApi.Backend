# School API – Backend

Backend API desarrollada en **ASP.NET Core .NET 8**, siguiendo principios de **Arquitectura Limpia** y **DDD**, orientada a la gestión de cursos y lecciones para un entorno educativo.

El proyecto implementa autenticación segura mediante **JWT** y **BCrypt**, y utiliza **MySQL** como base de datos con **Entity Framework Core + Pomelo**.

---

## 🚀 Tecnologías utilizadas

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core
- MySQL
- Pomelo.EntityFrameworkCore.MySql
- JWT (JSON Web Tokens)
- BCrypt para hashing de contraseñas
- xUnit + Moq para pruebas unitarias
- Docker (en proceso de validación)
- GitHub Actions CI/CD (en proceso de validación)

---

## 🧱 Arquitectura

El proyecto sigue una estructura basada en **Clean Architecture + DDD**, separando responsabilidades en capas bien definidas:

``` 
Domain  
 └─ Entidades y lógica de dominio  

Application  
 ├─ Servicios de aplicación  
 ├─ Interfaces  
 └─ DTOs  

Infrastructure  
 ├─ Persistencia (EF Core, MySQL)  
 └─ Implementación de repositorios  

Api  
 ├─ Controllers  
 └─ Configuración de la aplicación  
```

Esta separación permite:
- Alta mantenibilidad
- Facilidad de testeo
- Bajo acoplamiento entre capas

---

## 🔐 Autenticación y seguridad

- Autenticación basada en JWT
- Claims incluidos en el token:
  - Email
  - Id del usuario (GUID)
  - Rol
- Hashing de contraseñas con BCrypt
- Implementación de Access Token + Refresh Token
- Endpoints protegidos mediante `[Authorize]`

---

## 📚 Funcionalidades principales

- Gestión de usuarios
- Gestión de cursos
  - Estados: Draft / Published
  - Publicación de cursos solo si tienen lecciones activas
  - Eliminación lógica (Soft Delete)
- Gestión de lecciones
  - Validación de orden único por curso
  - Eliminación lógica
- Autenticación y autorización

---

## 🧪 Pruebas unitarias

El proyecto incluye pruebas unitarias usando:

- xUnit
- Moq

Se testean reglas de negocio como:
- Publicación de cursos con y sin lecciones
- Creación de lecciones con orden único
- Eliminación lógica de cursos y lecciones

---

## 🐳 Docker y CI/CD

- Existe un Dockerfile y un docker-compose.yml para la ejecución del proyecto con base de datos MySQL en contenedores.
- Se ha configurado un workflow de GitHub Actions para construir y subir imágenes Docker al GitHub Container Registry (GHCR).

⚠️ Nota:  
Actualmente, la integración con Docker y el pipeline de CI/CD se encuentran en proceso de validación y pruebas.

---

## ⚙️ Configuración básica

### Variables de entorno (ejemplo)

```
ConnectionStrings__DefaultConnection=Server=db;Database=school_db;User=root;Password=yourpassword  
Jwt__Key=YOUR_SECRET_KEY_MIN_32_CHARS  
Jwt__Issuer=SchoolApi  
Jwt__Audience=SchoolApiUsers  
```
---

## ▶️ Ejecución local

```
dotnet restore  
dotnet build  
dotnet run --project src/Api  
```

La API estará disponible en:

```
https://localhost:5064
```
Swagger:

```
https://localhost:5064/swagger
```

---

## 📌 Estado del proyecto

Proyecto desarrollado con fines **educativos y de evaluación técnica**, enfocado en demostrar:

- Buen diseño de arquitectura
- Aplicación de DDD
- Seguridad en APIs
- Pruebas unitarias
- Buenas prácticas en .NET

---

## 👤 Autor

David Orjuela  
Backend Developer
