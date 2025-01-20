import { Component } from '@angular/core';
import { CuentasService } from '../../Service/cuentas.service';
import { CuentaDTO } from '../../Interfaces/CuentaDTO';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cuentas',
  standalone: false,
  
  templateUrl: './cuentas.component.html',
  styleUrl: './cuentas.component.css'
})
export class CuentasComponent {
  cuentas: any[] = [];
  searchText: string = '';
  constructor(private cuentasService: CuentasService , private router : Router) {}

  ngOnInit(): void {
    this.obtenerCuentas();
  }

  obtenerCuentas(): void {
    this.cuentasService.getAllCuentas().subscribe({
      next: (data) => {
        this.cuentas = data;
      },
      error: (err) => {
        console.error('Error al obtener las cuentas:', err);
      }
    });
  }

  crearCuenta(): void {
    // Navegar a la página/formulario para crear una nueva cuenta
    
    this.router.navigate(['/formularioCuentas'])
  
  }

 
  eliminarCuenta(idCuenta: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar esta cuenta?')) {
      this.cuentasService.deleteCuenta(idCuenta).subscribe({
        next: () => {
          console.log('Cuenta eliminada con éxito');
          this.obtenerCuentas(); // Recargar la lista de cuentas
        },
        error: (err) => {
          console.error('Error al eliminar la cuenta:', err);
        }
      });
    }
  }
}
