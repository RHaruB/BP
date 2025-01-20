import { Component } from '@angular/core';

@Component({
  selector: 'app-notificacion',
  standalone: false,
  
  templateUrl: './notificacion.component.html',
  styleUrl: './notificacion.component.css'
})
export class NotificacionComponent {
  message: string = '';
  type: 'success' | 'error' | '' = '';
  visible: boolean = false;

  showNotification(message: string, type: 'success' | 'error', duration: number = 3000): void {
    this.message = message;
    this.type = type;
    this.visible = true;

    setTimeout(() => {
      this.visible = false;
    }, duration);
  }
}
