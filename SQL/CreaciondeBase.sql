Create database BP ;
Use BP; 

CREATE TABLE Parametro(
	IdParametro INT IDENTITY(1,1) PRIMARY KEY, 
	Tipo INT NOT NULL,
	Clave VARCHAR(10),
	Valor VARCHAR(100),
	FechaIngreso DATE NOT NULL , 
	FechaActualizacion DATE
	);

CREATE TABLE Persona (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Genero INT NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Identificacion NVARCHAR(20) NOT NULL UNIQUE,
	TipoIdentificacion INT NOT NULL,
    Direccion NVARCHAR(200),
    Telefono NVARCHAR(15),
	FechaIngreso DATE NOT NULL , 
	FechaActualizacion DATE,
	 CONSTRAINT FK_Persona_Parametro FOREIGN KEY (Genero) REFERENCES Parametro(IdParametro),
	 CONSTRAINT FK_Persona_Parametro_Tipo_Identificacion FOREIGN KEY (TipoIdentificacion) REFERENCES Parametro(IdParametro)
);


CREATE TABLE Cliente (
    IdCliente INT IDENTITY(1,1) PRIMARY KEY, 
    PersonaID INT NOT NULL UNIQUE,
    Contrasena NVARCHAR(100) NOT NULL,
    Estado INT NOT NULL,
	FechaIngreso DATE NOT NULL , 
	FechaActualizacion DATE,
    CONSTRAINT FK_Cliente_Persona FOREIGN KEY (PersonaID) REFERENCES Persona(Id),
	CONSTRAINT FK_Cliente_Parametro FOREIGN KEY (Estado) REFERENCES Parametro(IdParametro)
);

CREATE TABLE Cuenta (
    IdCuenta INT IDENTITY(1,1) PRIMARY KEY,
	IdCliente INT NOT NULL,
    NumeroCuenta NVARCHAR(50) NOT NULL UNIQUE,
    Tipo INT NOT NULL, 
    SaldoInicial DECIMAL(18, 2) NOT NULL,
    Estado INT NOT NULL, 
	FechaIngreso DATE NOT NULL , 
	FechaActualizacion DATE,
    CONSTRAINT FK_Cuenta_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
	CONSTRAINT FK_Cuenta_Parametro FOREIGN KEY (Tipo) REFERENCES Parametro(IdParametro),
	CONSTRAINT FK_Cuenta_Parametro_Estado FOREIGN KEY (Estado) REFERENCES Parametro(IdParametro)
);

CREATE TABLE Movimiento (
    IdMovimiento INT IDENTITY(1,1) PRIMARY KEY,
	IdCuenta INT NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    TipoMovimiento INT NOT NULL, 
    Valor DECIMAL(18, 2) NOT NULL, 
    Saldo DECIMAL(18, 2) NOT NULL,   
	FechaIngreso DATE NOT NULL , 
	FechaActualizacion DATE,
    CONSTRAINT FK_Movimiento_Cuenta FOREIGN KEY (IdCuenta) REFERENCES Cuenta(IdCuenta),
	CONSTRAINT FK_Movimiento_Parametro FOREIGN KEY (TipoMovimiento) REFERENCES Parametro(IdParametro)
);


