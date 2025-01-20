import { Component } from '@angular/core';
import { MovimientoDTO } from '../../../Interfaces/MovimientoDTO';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MovimientosService } from '../../../Service/movimientos.service';
import { CuentasService } from '../../../Service/cuentas.service';
import { Router } from '@angular/router';
import { CuentaDTO } from '../../../Interfaces/CuentaDTO';

@Component({
  selector: 'app-form-movimiento',
  standalone: false,
  
  templateUrl: './form-movimiento.component.html',
  styleUrl: './form-movimiento.component.css'
})
export class FormMovimientoComponent {
  movimientoForm!: FormGroup;
  cuentas: CuentaDTO[] = [];
  tiposMovimiento = [
    { value: 16, label: 'Débito' },
    { value: 15, label: 'Crédito' },
  ];

  constructor(
    private fb: FormBuilder,
    private movimientosService: MovimientosService,
    private cuentasService: CuentasService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.movimientoForm = this.fb.group({
      idCuenta: ['', Validators.required],
      tipoMovimiento: ['', Validators.required],
      valor: ['', [Validators.required, Validators.min(0.01)]],
    });

    this.cargarCuentas();
  }

  cargarCuentas(): void {
    this.cuentasService.getAllCuentas().subscribe({
      next: (data) => {
        this.cuentas = data;
      },
      error: (error) => {
        console.error('Error al cargar cuentas', error);
        alert('No se pudieron cargar las cuentas');
      },
    });
  }

  onSubmit(): void {
    if (this.movimientoForm.valid) {
      const movimiento: MovimientoDTO = this.movimientoForm.value;

      this.movimientosService.createMovimiento(movimiento).subscribe({
        next: () => {
          alert('Movimiento registrado correctamente');
          this.router.navigate(['/movimientos']);
        },
        error: (error) => {
          console.error('Error al registrar movimiento', error);
          alert('Hubo un error al registrar el movimiento');
        },
      });
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.movimientoForm.get(fieldName);
    return !!field && field.invalid && (field.dirty || field.touched);
  }

  onCancel(): void {
    this.router.navigate(['/movimientos']);
  }
}
