# ProyectoFSD_Movies

## 1.- Integrantes
- Flores Luis
- Reyes Diego
- Castro Andres

## 2.- Instrucciones y contenido.
El contenido a continuación a presentarse con las instrucciones y el contenido de la aplicación se 
encuentra también dentro del PDF `Entrega Final - FSD Backend Pro_Flores_Reyes_Castro.pdf`

## 3.- Conexion a base datos
  - 1) Cambiar el nombre del string de conexión para que coincida con la de su servidor en el archivo: appsettings.json y 
  appsettings.Development.json
  - 2) En el Visual Studio: Ir a Compilar > Recompilar Solución para evitar problemas en la ejecución.
  - 3) En Visual Studio: Ir a Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes.
  - 4)  Ejecutar el comando: Update-Database.
Con ese comando se permitirá ejecutar las migraciones correspondientes y en su SQL Server Management Studio podrá ver la base de datos creada.

## 4.- Script SQL para poblar base de datos.
Una vez realizadas las migraciones, por favor revisar que el esquema de base
datos se encuentre ya creado en su Servidor SQL. Con la base creada, ejecute
el script de sql `Datos_Proyecto.sql` para poblar las tablas con información
para las pruebas

## 5.- Información a detalle del Proyecto:
### 5.1.- Descripción general
CineManía es una aplicación web desarrollada con ASP.NET Core MVC y Entity
Framework Core. Esta primera entrega tiene como objetivo mostrar un avance
funcional que incluye la estructura del modelo de datos, funcionalidades CRUD
básicas, relaciones entre entidades y navegación entre ellas.

### 5.2.- Modelos y relaciones
Se han creado los siguientes modelos con sus atributos principales:
- Película: título, sinopsis, duración, fecha de estreno, imagen, género, director.
- Género: nombre, descripción.
- Director: nombre, nacionalidad, fecha de nacimiento, imagen.
- Actor: nombre, biografía, fecha de nacimiento, imagen.
Cada modelo ya cuenta con su controlador, vistas y su correspondiente tabla en la base de datos.

En este caso se manejaron las siguientes relaciones:
- Género – Película (1-N): Se consideró esta relación bajo la premisa que un género puede aparecer en varias películas, pero una película como tal esta asignada a un género en concreto.
- Director – Película (1-N): Se consideró esta relación bajo la premisa que un director puede tener más de una película que haya dirigido, pero una Película como tal es dirigida generalmente por un director.
- Actor – Película (M – N): Esta relación se creó con la premisa de que un actor puede participar en más de una película, y a su vez, una película puede tener varios actores en su cast. 
Nota: La relación Actor – Película se creó, pero no fue desarrollada en la entrega final; sin embargo, el código presentado permite crear la tabla “ActorPelicula” que las relaciona dentro de la base de datos usando las migraciones dentro de la base de datos; a la vez que esto no incide en el correcto funcionamiento de los modelos presentados.

### 5.3.- Funcionalidad CRUD
Se implementó el CRUD completo (crear, leer, editar, eliminar) para:
- Actores
- Directores
- Películas
- Géneros
Cada una de las vistas desarrolladas para cada uno de los modelos permite realizar las operaciones CRUD: Listar los diferentes registros de la base, pudiendo navegar y ver los detalles de los registros, editarlos o eliminarlos.

Por otro lado, cada vista de lista cuenta con un filtro de búsqueda para buscar cada modelo:
- Actores: Nombre
- Género: Nombre
- Películas: Título
- Director: Nombre

### 5.4.- Asociación y navegación entre entidades
- Al crear o editar una película se puede seleccionar su género y su
director a través de menés desplegables que muestran los nombres,
no los IDs.

- En la lista de películas ya aparecen los nombres del director y del
género asociados, en las diferentes vistas. En este contexto, la creacio n
de director y un género de pelí cula no estara anclado con la creacio n
del libro.

### 6.- Información Adicinional - Entregable 1 del proyecto
Dentro de la carpeta `Entrega 1 - Proyecto` se encontrará las instrucciones y archivos anteriores 
del entregable 1 del proyecto (README.md, reporte pdf y repositorio)


