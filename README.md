# Descripción del Reto

En CoWorkSpace, estamos desarrollando una plataforma para gestionar reservas en espacios de coworking. Necesitamos una API que permita a los usuarios:

- Registrarse.
- Consultar la disponibilidad de salas de reuniones o escritorios privados.
- Realizar y cancelar reservas bajo ciertas condiciones.

## Requerimientos Funcionales

### 1. Gestión de Usuarios
- Registrar un usuario con los siguientes campos:
  - Nombre
  - Email
  - Contraseña (encriptada)
- Autenticación mediante JWT o API Key para acceder a los endpoints.

### 2. Gestión de Espacios de Coworking
- Listar los espacios de coworking disponibles.
- Consultar detalles de un espacio específico.

### 3. Gestión de Reservas
- Permitir a los usuarios autenticados crear una reserva seleccionando fecha y horario.
- Un usuario solo puede tener una reserva activa por día.
- No se permiten reservas en fechas pasadas.
- Se puede cancelar una reserva con al menos una hora de anticipación.

---

## Requisitos Técnicos

- Lenguaje y framework de libre elección.
- Base de datos: MySQL o PostgreSQL.
- Autenticación mediante JWT o API Key.
- Validación de datos y cifrado de contraseñas.
- Arquitectura modular siguiendo principios SOLID y Clean Architecture.
- Uso de Docker.
- Documentación con Swagger o Postman.

---

## Instrucciones para Levantar el Proyecto

### Levantar el Proyecto en Local

1. Clonar el repositorio:
```bash
git clone https://github.com/Estefano-Lostaunau/coworkspace.git
cd coworkspace/coworkspace.Api
```

2. Configurar la base de datos:

Editar el archivo `appsettings.json` y establecer la cadena de conexión para MySQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=coworkspace;User=root;Password=root;"
  }
}
```

3. Restaurar dependencias:
```bash
dotnet restore
```

4. Ejecutar el proyecto:
```bash
dotnet run
```

La aplicación estará disponible en: **http://localhost:5252** (o el puerto configurado en la API).

---

### Levantar el Proyecto con Docker

1. Navegar a la carpeta del proyecto:
```bash
cd coworkspace
```

2. Configurar la base de datos:

Editar el archivo `appsettings.json` y establecer la cadena de conexión para MySQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=db;Port=3306;Database=coworkspace;User=root;Password=root;"
  }
}
```

3. Construir la imagen del proyecto:
```bash
docker-compose build
```

4. Levantar los contenedores Docker:
```bash
docker-compose up -d
```

5. Verificar que la aplicación esté corriendo:
La API estará disponible en: **http://localhost:8080**.

Si se desea detener los contenedores:
```bash
docker-compose down
```

---


## Documentación y Pruebas
- Para probar los endpoints, se puede usar Postman o acceder a la documentación en Swagger:
  - Swagger(local): [http://localhost:5252/index.html](http://localhost:5252/index.html)
  - Swagger(docker): [http://localhost:8080/index.html](http://localhost:8080/index.html)

---

## Notas Adicionales
- Asegurarse de tener MySQL configurado correctamente en el entorno.

