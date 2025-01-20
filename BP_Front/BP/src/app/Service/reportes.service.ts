import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportesService {
    urlBase= environment.urlBase
  controlador = 'Reporte/'
  private apiUrl = this.urlBase + this.controlador; 

  constructor(private http: HttpClient) {}

  generarReporteEstadoCuenta(fechaInicio: string, fechaFin: string, clienteId: number): Observable<any> {
    const url = `${this.apiUrl}`; 
    const params = {
      fechaInicio,
      fechaFin,
      idCliente: clienteId.toString(),
    };
    return this.http.post<any>(url, params);
  }
}
