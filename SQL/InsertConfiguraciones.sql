INSERT INTO Parametro (Tipo, Clave, Valor, FechaIngreso, FechaActualizacion)
VALUES
(0 , '1', 'Generos', GETDATE(), GETDATE()),
(0 , '2 ', 'Tipos de identificaci�n', GETDATE(), GETDATE()),
(0 , '3', 'Estados', GETDATE(), GETDATE()),
(0 , '4', 'Tipos de Cuenta', GETDATE(), GETDATE()),
(0 , '5', 'Tipos de movimiento', GETDATE(), GETDATE()),
-- G�neros
(1, 'M', 'Masculino', GETDATE(), GETDATE()),
(1, 'F', 'Femenino', GETDATE(), GETDATE()),
-- Tipos de Identificaci�n
(2, 'C', 'C�dula de Ciudadan�a', GETDATE(), GETDATE()),
(2, 'R', 'Ruc', GETDATE(), GETDATE()),
(2, 'P', 'Pasaporte', GETDATE(), GETDATE()),
-- Estados
(3, '1', 'Activo', GETDATE(), GETDATE()),
(3, '0', 'Inactivo', GETDATE(), GETDATE()),
-- Tipos de Cuenta
(4, 'AH', 'Ahorro', GETDATE(), GETDATE()),
(4, 'CO', 'Corriente', GETDATE(), GETDATE()),
-- Tipos de Movimiento
(5, 'CR', 'Cr�dito', GETDATE(), GETDATE()),
(5, 'DB', 'D�bito', GETDATE(), GETDATE());


INSERT INTO Parametro (Tipo, Clave, Valor, FechaIngreso, FechaActualizacion)
VALUES
(0 , '6', 'Numero cuenta', GETDATE(), GETDATE()),
(6 , 'cta', '2200000000', GETDATE(), GETDATE());


INSERT INTO Parametro (Tipo, Clave, Valor, FechaIngreso, FechaActualizacion)
VALUES
(0 , '6', 'Numero cuenta', GETDATE(), GETDATE()),
(6 , 'cta', '2200000000', GETDATE(), GETDATE());
