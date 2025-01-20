import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MovimientoDTO } from '../Interfaces/MovimientoDTO';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MovimientosService {
  private apiUrl = 'https://localhost:44364/api/Movimientos';

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
