import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportesService {
  private apiUrl = 'https://localhost:44364/api/Reporte'; // Cambia esto por la URL de tu API

  constructor(private http: HttpClient) {}

  // Método para obtener el reporte en formato base64
  generarReporteEstadoCuenta(fechaInicio: string, fechaFin: string, clienteId: number): Observable<any> {
    const url = `${this.apiUrl}`; // Cambia según la ruta de tu controlador
    const params = {
      fechaInicio,
      fechaFin,
      idCliente: clienteId.toString(),
    };
    return this.http.post<any>(url, params);
  }
}
