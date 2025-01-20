import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CuentaDTO } from '../Interfaces/CuentaDTO';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CuentasService {

   urlBase= environment.urlBase
      controlador = 'Cuentas'
      private apiUrl = this.urlBase + this.controlador; 

  constructor(private http: HttpClient) {}

  getAllCuentas(): Observable<CuentaDTO[]> {
    return this.http.get<CuentaDTO[]>(`${this.apiUrl}`);
  }

  deleteCuenta(idCuenta: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${idCuenta}`);
  }
  createCuenta(cuentaData: any): Observable<any> {
    console.log(cuentaData);
    
    return this.http.post(`${this.apiUrl}`, cuentaData);
  }

  updateCuenta(cuentaData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}`, cuentaData);
  }
}
