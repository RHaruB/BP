import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientesService } from '../../../Service/clientes.service';
import { ClienteDTO } from '../../../Interfaces/ClienteDTO';
import { NotificacionComponent } from '../../General/notificacion/notificacion.component';


@Component({
  selector: 'app-form-cliente',
  standalone: false,
  
  templateUrl: './form-cliente.component.html',
  styleUrl: './form-cliente.component.css'
})
export class FormClienteComponent {
  @ViewChild(NotificacionComponent) notification!: NotificacionComponent;
  clienteForm: FormGroup;
  isEditing: boolean = false;
  clienteId: number | null = null;
  
  genderOptions = [
    { value: '6', label: 'Masculino' },
    { value: '7', label: 'Femenino' }
  ];

  tipoIdentificacionOptions = [
    { value: '9', label: 'RUC' },
    { value: '10', label: 'Pasaporte' },
    { value: '8', label: 'Cédula' },
  ];

  constructor(private fb: FormBuilder , private route: ActivatedRoute, private router : Router, private clientesService : ClientesService) {
    this.clienteForm = this.fb.group({
      nombre: ['', [Validators.required]],
      genero: ['', [Validators.required]],
      fechaNacimiento: ['', [Validators.required]],
      identificacion: ['', [Validators.required]],
      tipoIdentificacion: ['', [Validators.required]],
      direccion: ['', [Validators.required]],
      telefono: ['', [Validators.required]],
      contrasena: ['', [Validators.required]]
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.clienteForm.get(fieldName);
    return field ? (field.invalid && (field.dirty || field.touched)) : false;
  }

  ngOnInit(): void {
    // Detectar si se está editando a través del parámetro de ruta
    this.route.paramMap.subscribe((params: { get: (arg0: string) => any; }) => {
      const id = params.get('id');
      if (id) {
        this.isEditing = true;
        this.clienteId = +id;
        this.cargarCliente(this.clienteId);
      }
    });
  }

  cargarCliente(id: number): void {
    this.clientesService.getClienteById(id).subscribe({
      next: (cliente) => {
        this.clienteForm.patchValue({
          nombre: cliente.nombre,
          genero: cliente.genero,
          fechaNacimiento: cliente.fechaNacimiento,
          identificacion: cliente.identificacion,
          tipoIdentificacion: cliente.tipoIdentificacion,
          direccion: cliente.direccion,
          telefono: cliente.telefono,
          contrasena: cliente.contrasena
        });
      },
      error: (error) => {
        console.error('Error al cargar cliente', error);
      }
    });
  }

  onSubmit(): void {
    if (this.clienteForm.valid) {
      const cliente: ClienteDTO = this.clienteForm.value;

      if (this.isEditing && this.clienteId) {
        // Editar cliente
        this.clientesService.updateCliente(this.clienteId, cliente).subscribe({
          next: (data) => {
            this.notification.showNotification('Cliente actualizado correctamente', 'success');

            this.router.navigate(['/clientes']);
          },
          error: (error) => {
            console.error('Error al actualizar cliente', error);
            this.notification.showNotification('Hubo un error al registrar al cliente', 'error');

          }
        });
      } else {
        // Crear nuevo cliente
        this.clientesService.createCliente(cliente).subscribe({
          next: (data) => {
            this.notification.showNotification('Cliente registrado correctamente', 'success');

            this.router.navigate(['/clientes']);
          },
          error: (error) => {
            console.error('Error al crear cliente', error);
            this.notification.showNotification('Hubo un error al registrar al cliente', 'error');
          }
        });
      }
    } else {
      this.notification.showNotification('Por favor completa todos los campos requeridos.', 'error');
    }
  }

  onCancel(): void {
    this.clienteForm.reset();

      this.router.navigate(['/clientes']);
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.values(formGroup.controls).forEach(control => {
      control.markAsTouched();
      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }
}
