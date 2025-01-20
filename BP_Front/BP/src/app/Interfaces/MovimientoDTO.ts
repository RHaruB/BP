export interface MovimientoDTO {
    idMovimiento: number;
    idCuenta: number;
    numeroCuenta: string;   
    fecha: Date;
    tipoMovimiento: number;
    tipoMovimientoDescripcion: string;
    valor: number;
    saldo: number;
    clienteNombre: string;
  }
  