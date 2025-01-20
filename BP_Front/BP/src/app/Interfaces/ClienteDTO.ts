export interface ClienteDTO {
    idCliente: number;
    id: number;
    nombre: string;
    genero: number;
    generoDescripcion: string;
    fechaNacimiento: string; // Utilizamos string por formato ISO (YYYY-MM-DD)
    identificacion: string;
    tipoIdentificacion: number;
    tipoIdentificacionDescripcion: string;
    direccion: string;
    telefono: string;
    contrasena: string;
    estado: number;
    estadoDescripcion: string;
    edad: number;
  }