CREATE DATABASE transportes_guzman
GO
USE transportes_guzman
GO

CREATE TABLE tipos_permiso (
	nombre_permiso VARCHAR(5) NOT NULL,
	CONSTRAINT PKTiposPermiso PRIMARY KEY (nombre_permiso)
)
GO

CREATE TABLE permisos_conducir (
	numero_permiso CHAR(9) NOT NULL,
	tipo_permiso VARCHAR(5) NOT NULL,
	caducidad DATE NOT NULL,
	CONSTRAINT PKPermisosConducir PRIMARY KEY (numero_permiso),
	CONSTRAINT FKPermisosTipoPermiso FOREIGN KEY (tipo_permiso)
		REFERENCES tipos_permiso (nombre_permiso)
)
GO

-- Como el dni y el permiso son iguales y referenciar una foreign key permite duplicados,
-- he creado un código de empleado único para evitarlo.
CREATE TABLE transportistas (
	id_empleado UNIQUEIDENTIFIER NOT NULL,
	permiso_dni CHAR(9) NOT NULL,
	nombre VARCHAR(20) NOT NULL,
	apellidos VARCHAR(50) NOT NULL,
	anio_nacimiento SMALLINT NOT NULL,
	CONSTRAINT PKTransportistas PRIMARY KEY (id_empleado),
	CONSTRAINT FKTransportistasPermisosConducir FOREIGN KEY (permiso_dni)
		REFERENCES permisos_conducir (numero_permiso),
	CONSTRAINT CHK_Dni CHECK (permiso_dni LIKE REPLICATE('[0-9]', 8) + '[A-Z]')
)
GO

CREATE TABLE vehiculos (
	matricula CHAR(7) NOT NULL,
	modelo VARCHAR(40) NOT NULL,
	permiso_necesario VARCHAR(5) NOT NULL,
	CONSTRAINT PKVehiculos PRIMARY KEY (matricula),
	CONSTRAINT FKVehiculosTiposPermiso FOREIGN KEY (permiso_necesario)
		REFERENCES tipos_permiso (nombre_permiso),
	CONSTRAINT CHK_Matricula CHECK (matricula LIKE REPLICATE('[0-9]', 4) + REPLICATE('[BCDFGHJKLMNPRSTVWXYZ]', 3))
)
GO

CREATE TABLE provincias (
	id_provincia SMALLINT NOT NULL,
	nombre_provincia VARCHAR(30) NOT NULL,
	CONSTRAINT PKProvincias PRIMARY KEY (id_provincia)
)
GO

CREATE TABLE ruta (
	numero_ruta INT IDENTITY NOT NULL,
	id_conductor UNIQUEIDENTIFIER NULL,
	matricula_vehiculo CHAR(7) NULL,
	-- remolque NULL?
	provincia_origen SMALLINT NOT NULL,
	provincia_destino SMALLINT NOT NULL,
	km_recorridos INT NULL,
	CONSTRAINT PKRutasRealizadas PRIMARY KEY (numero_ruta),

	CONSTRAINT FKRutasTransportistas FOREIGN KEY (id_conductor)
		REFERENCES transportistas (id_empleado),

	CONSTRAINT FKRutasVehiculos FOREIGN KEY (matricula_vehiculo)
		REFERENCES vehiculos (matricula),

	CONSTRAINT FKRutasProvinciaOrigen FOREIGN KEY (provincia_origen)
		REFERENCES provincias (id_provincia),
	CONSTRAINT FKRutasProvinciaDestino FOREIGN KEY (provincia_destino)
		REFERENCES provincias (id_provincia)
)
GO

-- FUNCIONES ------------------------

CREATE OR ALTER FUNCTION KmRecorridosDeVehiculo(@matricula CHAR(7))
RETURNS INT
AS BEGIN
	DECLARE @Kms INT
	SELECT @Kms = SUM(km_recorridos) FROM rutas_realizadas
	WHERE matricula_vehiculo = @matricula
	RETURN @Kms
END
GO

CREATE OR ALTER FUNCTION VehiculosKmsRecorridos()
RETURNS TABLE AS
RETURN
SELECT VE.matricula, VE.modelo, SUM(ISNULL(RU.km_recorridos,0)) AS KmRecorridos FROM vehiculos AS VE
LEFT JOIN rutas_realizadas AS RU
	ON RU.matricula_vehiculo = VE.matricula
GROUP BY VE.matricula, VE.modelo
GO

CREATE OR ALTER FUNCTION DisplayRutas()
RETURNS TABLE AS
RETURN
SELECT 
	RU.numero_ruta, 
	TR.permiso_dni AS dni_conductor, 
	TR.nombre AS nombre_conductor,
	TR.apellidos AS apellidos_conductor,
	RU.matricula_vehiculo,
	PRO.nombre_provincia AS provincia_origen,
	PRD.nombre_provincia AS provincia_destino,
	ISNULL(RU.km_recorridos,0) AS km_recorridos
FROM rutas AS RU
LEFT JOIN transportistas AS TR
	ON RU.id_conductor = TR.id_empleado
INNER JOIN provincias AS PRO
	ON RU.provincia_origen = PRO.id_provincia
INNER JOIN provincias AS PRD
	ON RU.provincia_destino = PRD.id_provincia
GROUP BY RU.numero_ruta, TR.permiso_dni, TR.nombre, TR.apellidos RU.matricula_vehiculo,
	RU.km_recorridos, PRO.nombre_provincia, PRD.nombre_provincia
GO


-- INSERTS --------------------------

INSERT INTO provincias VALUES
	(2,'Albacete'),
	(3,'Alicante/Alacant'),
	(4,'Almería'),
	(1,'Araba/Álava'),
	(33,'Asturias'),
	(5,'Ávila'),
	(6,'Badajoz'),
	(7,'Balears, Illes'),
	(8,'Barcelona'),
	(48,'Bizkaia'),
	(9,'Burgos'),
	(10,'Cáceres'),
	(11,'Cádiz'),
	(39,'Cantabria'),
	(12,'Castellón/Castelló'),
	(51,'Ceuta'),
	(13,'Ciudad Real'),
	(14,'Córdoba'),
	(15,'Coruña, A'),
	(16,'Cuenca'),
	(20,'Gipuzkoa'),
	(17,'Girona'),
	(18,'Granada'),
	(19,'Guadalajara'),
	(21,'Huelva'),
	(22,'Huesca'),
	(23,'Jaén'),
	(24,'León'),
	(27,'Lugo'),
	(25,'Lleida'),
	(28,'Madrid'),
	(29,'Málaga'),
	(52,'Melilla'),
	(30,'Murcia'),
	(31,'Navarra'),
	(32,'Ourense'),
	(34,'Palencia'),
	(35,'Palmas, Las'),
	(36,'Pontevedra'),
	(26,'Rioja, La'),
	(37,'Salamanca'),
	(38,'Santa Cruz de Tenerife'),
	(40,'Segovia'),
	(41,'Sevilla'),
	(42,'Soria'),
	(43,'Tarragona'),
	(44,'Teruel'),
	(45,'Toledo'),
	(46,'Valencia/València'),
	(47,'Valladolid'),
	(49,'Zamora'),
	(50,'Zaragoza')
GO

INSERT INTO tipos_permiso VALUES
	('A1'),('A2'),('A'),('B'),('BTP'),('B+E'),('C1'),('C1+E'),('C'),('C+E'),('D1'),('D1+E'),('D')
GO

INSERT INTO permisos_conducir VALUES
	('80976604K','B','10-10-2025')
	,('93678360G','D','10-2-2025')
	,('08960454E','C','8-2-2023')
	,('80013971F','C1+E','10-9-2022')
	,('00000001A','A1','25-12-2022')
GO

INSERT INTO transportistas VALUES
	(NEWID(), '80976604K', 'Adán', 'Dando', 1999)
	,(NEWID(), '93678360G', 'Tomás', 'Todonte', 1977)
	,(NEWID(), '08960454E', 'Daniel', 'Truhán', 1987)
	,(NEWID(), '80013971F', 'Pedro', 'Medario', 1965)
	,(NEWID(), '00000001A', 'Jesus', 'Josephson', 0)
GO

INSERT INTO vehiculos VALUES
	('1292JFJ', 'Kawasaki Ninja','A')
	,('8289DWD', 'Iveco Transporter', 'C1')
	,('8008PWD', 'Citroën C15', 'B')
GO


INSERT INTO rutas VALUES
	('65211846-8355-4C6E-A210-2B7B63B2E6F0','1292JFJ', 51, 43, 900)
	,('21411820-8BCA-41A8-A5A5-5B954919F1D7','8289DWD', 43, 51, 200)
	,('E7526A5E-41B4-42A0-8CF0-65E940936EE7','8289DWD', 51, 51, 100)

