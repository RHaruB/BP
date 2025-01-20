import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClienteDTO } from '../Interfaces/ClienteDTO';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClientesService {
   urlBase= environment.urlBase
        controlador = 'Clientes'
        private apiUrl = this.urlBase + this.controlador; 

  constructor(private http: HttpClient) {}

  getAllClientes(): Observable<ClienteDTO[]> {
    return this.http.get<ClienteDTO[]>(`${this.apiUrl}`);
  }

  deleteCliente(idCuenta: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${idCuenta}`);
  }
  createCliente(cuentaData: any): Observable<any> {
    console.log(cuentaData);
    
    return this.http.post(`${this.apiUrl}`, cuentaData);
  }

  updateCliente(id: number, cliente: ClienteDTO): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, cliente);
  }
  getClienteById(id: number): Observable<ClienteDTO> {
    return this.http.get<ClienteDTO>(`${this.apiUrl}/${id}`);
  }
}
