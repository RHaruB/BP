import { Component } from '@angular/core';
import { MovimientoDTO } from '../../Interfaces/MovimientoDTO';
import { MovimientosService } from '../../Service/movimientos.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-movimientos',
  standalone: false,
  
  templateUrl: './movimientos.component.html',
  styleUrl: './movimientos.component.css'
})
export class MovimientosComponent {
  movimientos: MovimientoDTO[] = [];
  filteredMovimientos: MovimientoDTO[] = [];

  constructor(private router : Router,private movimientosService: MovimientosService) {}

  ngOnInit(): void {
    this.getMovimientos();
  }

  getMovimientos(): void {
    this.movimientosService.getAllMovimientos().subscribe({
      next: (data) => {
        this.movimientos = data;
        this.filteredMovimientos = data;
      },
      error: (err) => console.error(err),
    });
  }

  onSearch(query: string): void {
    this.filteredMovimientos = this.movimientos.filter((movimiento) =>
      movimiento.clienteNombre.toLowerCase().includes(query.toLowerCase())
    );
  }

  crearMovimiento(): void {
    this.router.navigate(['/formularioMovimientos'])
  }

  editarMovimiento(movimiento: MovimientoDTO): void {
    // Lógica para editar un movimiento
  }

  eliminarMovimiento(idMovimiento: number): void {
    // Lógica para eliminar un movimiento
    this.movimientosService.deleteMovimiento(idMovimiento).subscribe({
      next: () => this.getMovimientos(),
      error: (err) => console.error(err),
    });
  }
  
}
