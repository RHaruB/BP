import { Component } from '@angular/core';
import { ReportesService } from '../../Service/reportes.service';
import { ClientesService } from '../../Service/clientes.service';

@Component({
  selector: 'app-reportes',
  standalone: false,
  
  templateUrl: './reportes.component.html',
  styleUrl: './reportes.component.css'
})
export class ReportesComponent   {
  fechaInicio: string = '';
  fechaFin: string = '';
  clienteId: number = 1; // Asegúrate de usar un cliente válido
  pdfBase64: string = '';
  error: string = '';
  
  clientes : any;
  constructor(private reportService: ReportesService , private clienteService: ClientesService) {}
  
  ngOnInit(): void {
    this.cargarClientes();
  }

  // Método para generar el reporte
  generarReporte() {
    if (this.fechaInicio && this.fechaFin) {
      this.reportService.generarReporteEstadoCuenta(this.fechaInicio, this.fechaFin, this.clienteId).subscribe(
        (response) => {
          // El reporte fue generado exitosamente
          this.pdfBase64 = response.pdfBase64;
          this.error = '';
          this.descargarPdf();
        },
        (error) => {
          // Manejar el error
          console.error(error);
          this.error = 'Ocurrió un error al generar el reporte.';
        }
      );
    } else {
      this.error = 'Por favor, ingresa un rango de fechas válido.';
    }
  }

  // Método para descargar el PDF
  descargarPdf() {
    const link = document.createElement('a');
    link.href = `data:application/pdf;base64,${this.pdfBase64}`;
    link.download = 'estado_cuenta.pdf'; // El nombre del archivo
    link.click();
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
