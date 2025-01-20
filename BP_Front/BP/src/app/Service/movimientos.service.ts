import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MovimientoDTO } from '../Interfaces/MovimientoDTO';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovimientosService {
     urlBase= environment.urlBase
    controlador = 'Movimientos'
    private apiUrl = this.urlBase + this.controlador; 

  constructor(private http: HttpClient) {}

  getAllMovimientos(): Observable<MovimientoDTO[]> {
    return this.http.get<MovimientoDTO[]>(`${this.apiUrl}`);
  }

  deleteMovimiento(idMovimiento: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Delete/${idMovimiento}`);
  }
  createMovimiento(movimiento: MovimientoDTO): Observable<any> {
    return this.http.post(`${this.apiUrl}`, movimiento);
  }
}
