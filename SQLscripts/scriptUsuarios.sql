USE transportes_guzman
GO

CREATE TABLE usuarios (
	id_usuario INT IDENTITY(1,1) NOT NULL,
    nombre_usuario VARCHAR(40) NOT NULL UNIQUE,
    hash_passwd BINARY(64) NOT NULL,
    CONSTRAINT PKUsuario PRIMARY KEY (id_usuario)
)
GO

CREATE OR ALTER PROCEDURE addUsuario
    @nombreUsuario NVARCHAR(50), 
    @password NVARCHAR(50),
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
    BEGIN TRY

        INSERT INTO usuarios (nombre_usuario, password)
        VALUES(@nombreUsuario, @password)
        SET @responseMessage='Exito'
    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH
END
GO

CREATE OR ALTER PROCEDURE validateUser
      @nombreUsuario NVARCHAR(50),
      @password NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @id INT
     
	SELECT @id = id_usuario
	FROM usuarios WHERE nombre_usuario = @nombreUsuario AND [password] = @password
     
	IF @id IS NOT NULL
	BEGIN
		SELECT @id id_usuario -- Usuario válido
	END
	ELSE
	BEGIN
		SELECT -1 -- Usuario inválido
	END
END
GO
	