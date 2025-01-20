import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CuentasService } from '../../../Service/cuentas.service';
import { Router } from '@angular/router';
import { ClientesService } from '../../../Service/clientes.service';

@Component({
  selector: 'app-form-cuenta',
  standalone: false,
  
  templateUrl: './form-cuenta.component.html',
  styleUrl: './form-cuenta.component.css'
})
export class FormCuentaComponent {
  cuentaForm!: FormGroup;
  isEditing = false;
  tipoOptions = [
    { value: '13', label: 'Ahorro' },
    { value: '14', label: 'Corriente' }
  ];
  estadoOptions = [
    { value: '11', label: 'Activo' },
    { value: '12', label: 'Inactivo' }
  ];
  clientes : any;

  constructor(private fb: FormBuilder, private cuentaService: CuentasService ,  private router : Router , private clienteService: ClientesService) {}

  ngOnInit(): void {
    this.cuentaForm = this.fb.group({
      idCliente: [null, [Validators.required]],
      tipo: ['', [Validators.required]],
      saldoInicial: [null, [Validators.required, Validators.min(0)]],
    });
    this.cargarClientes();
  }

  // isFieldInvalid(field: string): boolean {
  //   const control = this.cuentaForm.get(field);
  //   return control?.invalid && (control.dirty || control.touched);
  // }
  isFieldInvalid(fieldName: string): boolean {
    const field = this.cuentaForm.get(fieldName);
    return field ? (field.invalid && (field.dirty || field.touched)) : false;
  }

  onSubmit(): void {
    if (this.cuentaForm.valid) {
      const cuentaData = this.cuentaForm.value;

      if (this.isEditing) {
        this.cuentaService.updateCuenta(cuentaData).subscribe(() => {
          console.log('Cuenta actualizada exitosamente.');
          this.router.navigate(['/cuentas']);
        });
      } else {
        this.cuentaService.createCuenta(cuentaData).subscribe(() => {
          console.log('Cuenta creada exitosamente.');
          this.router.navigate(['/cuentas']);
        });
      }
    }
  }

  onCancel(): void {
    this.cuentaForm.reset();
    this.router.navigate(['/cuentas']);
  }
  cargarClientes(): void {
    this.clienteService.getAllClientes().subscribe({
      next: (data) => {
        this.clientes = data;
      },
      error: (error) => {
        console.error('Error al cargar cuentas', error);
        alert('No se pudieron cargar las cuentas');
      },
    });
  }

}
