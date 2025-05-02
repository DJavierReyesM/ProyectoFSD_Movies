# ProyectoFSD_Movies

## Integrantes
- Flores Luis
- Reyes Diego
- Castro Andres

## Conexion a base datos
Cambiar el nombre de la conexión para que coincida con la de su servidor en el archivo: appsettings.Development

## Información a detalle:
### Descripción general
CineManía es una aplicación web desarrollada con ASP.NET Core MVC y Entity
Framework Core. Esta primera entrega tiene como objetivo mostrar un avance
funcional que incluye la estructura del modelo de datos, funcionalidades CRUD
básicas, relaciones entre entidades y navegación entre ellas.

### Modelos y relaciones
Se han creado los siguientes modelos con sus atributos principales:
- Película: tí tulo, sinopsis, duracio n, fecha de estreno, imagen, ge nero,
director.
- Género: nombre, descripcio n.
- Director: nombre, nacionalidad, fecha de nacimiento, imagen.
- Actor: nombre, biografí a, fecha de nacimiento, imagen.

Cada modelo ya cuenta con su controlador, vistas y su correspondiente tabla
en la base de datos.
En este caso se manejaron las siguientes relaciones:

- Género – Película (1-N): Se considero esta relacio n bajo la premisa
que un ge nero puede aparecer en varias pelí culas, pero una pelí cula
como tal esta asignada a un ge nero en concreto.
- Director – Película (1-N): Se considero esta relacio n bajo la premisa
que un director puede tener ma s de una pelí cula que haya dirigido, pero
una Pelí cula como tal es dirigida generalmente por un director.
- Actor – Película (M – N): Esta relacio n se creo con la premisa de que
un actor puede participar en ma s de una pelí cula, y a su vez, una
pelí cula puede tener varios actores en su cast.
Nota: La relación Actor – Película se creó pero no fue desarrollada en la
presente entrega, se espera tenerla lista para la entrega final.

### Funcionalidad CRUD
Se implemento el CRUD completo (crear, leer, editar, eliminar) para:
- Actores
- Directores
- Películas
- Generos
Cada vista de lista muestra los elementos sin duplicados y permite navegar a
editar, eliminar o ver detalles.
Asociación y navegación entre entidades
- Al crear o editar una pelí cula se puede seleccionar su ge nero y su
director a trave s de menu s desplegables que muestran los nombres,
no los IDs.
- En la lista de pelí culas ya aparecen los nombres del director y del
ge nero asociados, en las diferentes vistas. En este contexto, la creacio n
de director y un ge nero de pelí cula no estara anclado con la creacio n
del libro.
- Para una posterior entrega se plantea trabajar la relacio n Actores –
Pelí culas de cardinalidad N-M para robustecer las relaciones entre los
modelos.
