-- Script para crear la tabla de Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(50) NOT NULL
);

-- Script para crear la tabla de Publicaciones
CREATE TABLE Publicaciones (
    Id INT PRIMARY KEY,
    Titulo NVARCHAR(100) NOT NULL,
    Contenido NVARCHAR(MAX) NOT NULL,
    IdAutor INT NOT NULL,
    FechaEnvio DATETIME NOT NULL,
    PendienteAprobacion BIT NOT NULL,
    FOREIGN KEY (IdAutor) REFERENCES Usuarios(Id)
);

-- Consultas de ejemplo

-- Insertar un nuevo usuario
INSERT INTO Usuarios (Id, Nombre, Rol) VALUES (1, 'Usuario1', 'Escritor');

-- Insertar una nueva publicación
INSERT INTO Publicaciones (Id, Titulo, Contenido, IdAutor, FechaEnvio, PendienteAprobacion) 
VALUES (1, 'Título de la publicación', 'Contenido de la publicación', 1, GETDATE(), 1);

-- Consulta para obtener todas las publicaciones pendientes de aprobación
SELECT p.Id, p.Titulo, p.Contenido, u.Nombre AS NombreAutor, p.FechaEnvio
FROM Publicaciones p
JOIN Usuarios u ON p.IdAutor = u.Id
WHERE p.PendienteAprobacion = 1;

-- Consulta para aprobar una publicación pendiente
UPDATE Publicaciones SET PendienteAprobacion = 0 WHERE Id = 1;

-- Consulta para eliminar una publicación rechazada
DELETE FROM Publicaciones WHERE Id = 1;
