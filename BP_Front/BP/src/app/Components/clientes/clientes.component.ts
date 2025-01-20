import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ClientesService } from '../../Service/clientes.service';
import { NotificacionComponent } from '../General/notificacion/notificacion.component';

@Component({
  selector: 'app-clientes',
  standalone: false,
  
  templateUrl: './clientes.component.html',
  styleUrl: './clientes.component.css'
})
export class ClientesComponent {
  @ViewChild(NotificacionComponent) notification!: NotificacionComponent;

  searchTerm: string = '';
  
  // Sample data
  clients: any[] = [
    {
      name: 'Juan Pérez',
      gender: 'Masculino',
      age: 35,
      identification: '1234567890',
      address: 'Calle Principal 123',
      phone: '555-0123'
    },
    {
      name: 'María García',
      gender: 'Femenino',
      age: 28,
      identification: '0987654321',
      address: 'Avenida Central 456',
      phone: '555-4567'
    },
    {
      name: 'Carlos López',
      gender: 'Masculino',
      age: 42,
      identification: '5678901234',
      address: 'Plaza Mayor 789',
      phone: '555-8901'
    }
  ];
  constructor(  private router : Router,private clientesSvc : ClientesService) {
    
  }
  ngOnInit(): void {
    this.obtenerCuentas();
  }

  obtenerCuentas(): void {
    this.clientesSvc.getAllClientes().subscribe({
      next: (data) => {
        this.clients = data;
      },
      error: (err) => {
        console.error('Error al obtener las cuentas:', err);
        this.notification.showNotification('Error al obtener las cuentas', 'error');

      }
    });
  }

CrearCliente(){
  this.router.navigate(['/formularioclientes'])
}
eliminarCiente(idCuenta: number): void {
  if (confirm('¿Está seguro de que deseas eliminar el cliente?')) {
    this.clientesSvc.deleteCliente(idCuenta).subscribe({
      next: () => {
        console.log('Cliente eliminado con éxito');
        this.notification.showNotification('Cliente eliminado con éxito', 'success');

        this.obtenerCuentas(); // Recargar la lista de cuentas
      },
      error: (err) => {
        console.error('Error al eliminar el cliente:', err);
        this.notification.showNotification('Error al eliminar el cliente', 'error');

      }
    });
  }
}
editarCliente(id: number): void {
  this.router.navigate(['/clientes/editar', id]);
}
}
