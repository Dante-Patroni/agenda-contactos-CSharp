# ⚙️ Backend API - Agenda de Contactos

API RESTful desarrollada en **C# con ASP.NET Core** para la gestión de una agenda de contactos. Este backend proporciona un sistema seguro y estructurado para la autenticación de usuarios y el manejo de contactos, sirviendo como núcleo de datos para la aplicación móvil (Flutter).

---

## 👨‍🎓 Información Académica

- **Institución:** Instituto Universitario Aeronáutico (IUA)
- **Materia:** Programación de Dispositivos Móviles
- **Profesor:** Javier Leiva
- **Alumno:** Dante Patroni

---

## 🚀 Características Principales

- **Arquitectura Limpia (Clean Architecture):** Separación estricta de responsabilidades en 4 capas distintas para mayor escalabilidad y fácil mantenimiento.
- **Autenticación JWT:** Endpoints protegidos con Json Web Tokens (Bearer Validation).
- **Base de Datos Relacional:** Uso de MySQL gestionado a través de **Entity Framework Core**.
- **Documentación Interactiva:** Interfaz Swagger/OpenAPI disponible de forma nativa en el entorno de desarrollo.
- **Inyección de Dependencias:** Gestión eficiente de servicios, repositorios y contextos de datos.

---

## 🧩 Arquitectura del Proyecto (Clean Architecture)

El proyecto está estructurado en 4 proyectos de biblioteca de clases (`.csproj`) que representan las diferentes capas de la arquitectura:

1. **Backend.Domain (Dominio):**
   - Es el núcleo absoluto del sistema. No depende de ninguna otra capa tecnológica ni de frameworks externos.
   - Contiene las entidades principales (`User`, `Contacto`) y los contratos/interfaces de los repositorios (`IUserRepository`, `IContactoRepository`).

2. **Backend.Application (Aplicación):**
   - Contiene la lógica de negocio y los casos de uso.
   - Depende directamente del Dominio. Aquí se ubican los Servicios (`AuthService`, `ContactoService`, `JwtService`) y los DTOs (Data Transfer Objects) que encapsulan los datos requeridos por las APIs.

3. **Backend.Infrastructure (Infraestructura):**
   - Responsable de la persistencia de datos y del acceso externo a herramientas del sistema operativo.
   - Depende de la Aplicación y del Dominio. Contiene el contexto principal de Entity Framework (`AppDbContext`), el manejo de las migraciones y la implementación concreta de los repositorios (`UserRepository`, `ContactoRepository`).

4. **Backend.API (Presentación/API):**
   - Punto de entrada y salida de la aplicación HTTP.
   - Configura el contenedor de Inyección de Dependencias, los parámetros de autenticación JWT, Swagger y expone los `Controllers` que responden a las peticiones del cliente frontend (Flutter).

---

## 🛠️ Tecnologías y Herramientas

- **Framework:** .NET 8.0 / ASP.NET Core Web API
- **Base de Datos:** MySQL
- **ORM:** Entity Framework Core (`Pomelo.EntityFrameworkCore.MySql`)
- **Seguridad:** Autenticación JWT (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- **Documentación API:** Swashbuckle / Swagger

---

## ⚙️ Configuración y Ejecución

### 1. Requisitos Previos
- Instalar [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
- Instalar y tener en ejecución un servidor de **MySQL** local.

### 2. Configurar Base de Datos
En el proyecto `Backend.API`, el archivo `appsettings.json` contiene la cadena de conexión. Asegúrate de que las credenciales de MySQL coincidan con la configuración de tu entorno local (usuario y contraseña):

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=contactosdb;user=root;password=;"
}
```

### 3. Aplicar Migraciones
Para generar y poblar las tablas en tu base de datos MySQL mediante Code-First, abre una terminal en la raíz del proyecto y ejecuta:

```powershell
dotnet ef database update --project Backend.Infrastructure --startup-project Backend.API
```

### 4. Ejecutar el Proyecto
Puedes levantar el servidor backend directamente con el comando:

```powershell
dotnet run --project Backend.API
```
*Por defecto, la API se levantará localmente. Comprueba los puertos mapeados en tu consola (generalmente `http://localhost:5148/`).*

---

## 📡 Endpoints Principales

Con el proyecto en ejecución, puedes acceder a **Swagger** navegando a `http://localhost:<puerto>/swagger` para visualizar y probar todos los endpoints de forma interactiva.

**Autenticación:**
- `POST /api/Auth/login`: Genera un token JWT tras la validación exitosa de credenciales de un usuario.

**Contactos (Protegidos con Autorización JWT Bearer):**
- `GET /api/Contactos`: Recupera el listado completo de los contactos pertenecientes al usuario autenticado.
- `POST /api/Contactos`: Crea y persiste un nuevo contacto.
- `PUT /api/Contactos/{id}`: Actualiza los campos de un contacto existente.
- `DELETE /api/Contactos/{id}`: Remueve permanentemente un contacto de la base de datos.
