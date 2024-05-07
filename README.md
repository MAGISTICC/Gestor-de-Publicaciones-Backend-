# Gestor de Publicaciones (Test Aoniken)

Este proyecto es un test desarrollado para la empresa Aoniken. Proporciona una API REST para gestionar publicaciones de blog.

## Configuración

1. **Base de Datos:** La aplicación utiliza una base de datos SQL Server para almacenar las publicaciones y los usuarios. Se proporciona un script SQL para crear la estructura de la base de datos. Asegúrate de configurar la cadena de conexión en `appsettings.json` con los detalles de tu servidor SQL.

2. **Ejecución:** Para ejecutar la aplicación, abre el proyecto en Visual Studio y presiona F5. La aplicación se ejecutará en el puerto predeterminado (por ejemplo, `https://localhost:7226/`). Puedes probar los endpoints de la API utilizando herramientas como Postman.

## Endpoints de la API

- **Consultar Publicaciones Pendientes:**
  - Método: `GET`
  - Ruta: `/api/publicaciones/pendientes`
  - Descripción: Retorna una lista de publicaciones pendientes de aprobación.
  - Respuesta: Lista de publicaciones en formato JSON.

- **Aprobar Publicación:**
  - Método: `POST`
  - Ruta: `/api/publicaciones/aprobar`
  - Descripción: Aprueba una publicación pendiente de aprobación.
  - Parámetros: `idPublicacion` en el cuerpo de la solicitud.
  - Respuesta: Código de estado 200 si se aprueba correctamente.

- **Rechazar Publicación:**
  - Método: `POST`
  - Ruta: `/api/publicaciones/rechazar`
  - Descripción: Rechaza una publicación pendiente de aprobación.
  - Parámetros: `idPublicacion` en el cuerpo de la solicitud.
  - Respuesta: Código de estado 200 si se rechaza correctamente.

- **Actualizar Publicación:**
  - Método: `PUT`
  - Ruta: `/api/publicaciones/{id}`
  - Descripción: Actualiza una publicación existente.
  - Parámetros: `id` en la ruta y datos de la publicación actualizada en el cuerpo de la solicitud.
  - Respuesta: Publicación actualizada en formato JSON.

- **Eliminar Publicación:**
  - Método: `DELETE`
  - Ruta: `/api/publicaciones/{id}`
  - Descripción: Elimina una publicación existente.
  - Parámetros: `id` en la ruta.
  - Respuesta: Código de estado 200 si se elimina correctamente.

## Dependencias

- **Entity Framework Core:** Utilizado para interactuar con la base de datos.
- **Microsoft.AspNetCore.Mvc:** Proporciona soporte para crear servicios web API.

## Configuración de la Base de Datos

Se utilizó **XAMPP** para la conexión.

El siguiente esquema SQL puede ser utilizado para crear la estructura básica de la base de datos:

```sql
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100) NOT NULL,
    Rol VARCHAR(50) NOT NULL
);

CREATE TABLE Publicaciones (
    Id INT PRIMARY KEY IDENTITY,
    Titulo VARCHAR(255) NOT NULL,
    Contenido VARCHAR(1000) NOT NULL,
    IdAutor INT NOT NULL,
    FechaEnvio DATETIME NOT NULL,
    PendienteAprobacion BIT NOT NULL,
    FOREIGN KEY (IdAutor) REFERENCES Usuarios(Id)
);
