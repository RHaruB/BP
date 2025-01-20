export interface CuentaDTO {
    idCuenta: number;
    idCliente: number;
    numeroCuenta: string;
    tipo: string;
    tipoDescripcion: string;
    saldoInicial: number;
    estado: string;
    estadoDescripcion: string;
    clienteNombre: string;
  }