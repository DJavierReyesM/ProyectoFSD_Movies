Use ProyectoFSDPeliculas;

-- 1) Poblar Generos
INSERT INTO Generos (Nombre, Descripcion) VALUES
  ('Acci�n',          'Pel�culas con secuencias llenas de adrenalina y efectos visuales.'),
  ('Comedia',         'Pel�culas dise�adas para provocar risa y diversi�n.'),
  ('Drama',           'Historias serias con fuerte carga emocional.'),
  ('Ciencia Ficci�n', 'Argumentos basados en avances cient�ficos o futuros imaginarios.'),
  ('Terror',          'Pel�culas que buscan asustar o generar tensi�n.');

-- 2) Poblar Directores
INSERT INTO Directores (Nombre, Nacionalidad, FechaNacimiento) VALUES
  ('Quentin Tarantino',    'Estadounidense', '1963-03-27'),
  ('Christopher Nolan',    'Brit�nico',      '1970-07-30'),
  ('Guillermo del Toro',   'Mexicano',       '1964-10-09'),
  ('Hayao Miyazaki',       'Japon�s',        '1941-01-05'),
  ('Kathryn Bigelow',      'Estadounidense', '1951-11-27');

-- 3) Poblar Actores
INSERT INTO Actores (Nombre, Biografia, FechaNacimiento) VALUES
  ('Tom Hanks',             'Actor ganador de m�ltiples premios Oscar.',            '1956-07-09'),
  ('Meryl Streep',          'Considerada una de las mejores actrices de todos los tiempos.', '1949-06-22'),
  ('Leonardo DiCaprio',     'Conocido por su versatilidad y actuaciones intensas.', '1974-11-11'),
  ('Scarlett Johansson',    'Destacada tanto en cine independiente como en grandes blockbusters.', '1984-11-22'),
  ('Denzel Washington',     'Ganador de dos premios Oscar por sus interpretaciones dram�ticas.', '1954-12-28');

-- 4) Poblar Pel�culas
--   (t�tulo, sinopsis, duraci�n en minutos, fecha de estreno, imagen (URL o ruta), FK GeneroId, FK DirectorId)
INSERT INTO Peliculas (Titulo, Sinopsis, Duracion, FechaEstreno, GeneroId, DirectorId) VALUES
  ('Pulp Fiction',
      'Varias historias de crimen en Los �ngeles interconectadas de forma no lineal.',
      154, '1994-10-14',  1, 1),
  ('Inception',
      'Un ladr�n que roba secretos a trav�s de la invasi�n de los sue�os recibe un encargo inusual.',
      148, '2010-07-16', 4, 2),
  ('El Laberinto del Fauno',
      'Durante la posguerra espa�ola, una ni�a descubre un mundo fant�stico lleno de criaturas m�gicas.',
      118, '2006-10-11', 3, 3),
  ('El viaje de Chihiro',
      'Una ni�a entra en un mundo m�gico de esp�ritus y debe encontrar la forma de salvar a sus padres.',
      125, '2001-07-20', 4, 4),
  ('En Tierra Hostil',
      'La historia real de un m�dico del ej�rcito que ofrece ayuda en una zona de guerra sin arma.', 
      131, '2014-12-25',3, 5);

select top 10 * from Actores

select top 10 * from Generos

select top 10 * from Peliculas

select top 10 * from Directores
