import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PrincipalModule } from './principal/principal.module';
import { CuentasComponent } from './Components/cuentas/cuentas.component';
import { ClientesComponent } from './Components/clientes/clientes.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { FormClienteComponent } from './Components/clientes/form-cliente/form-cliente.component';
import { HttpClientModule } from '@angular/common/http';
import { MovimientosComponent } from './Components/movimientos/movimientos.component';
import { ReportesComponent } from './Components/reportes/reportes.component';
import { FormCuentaComponent } from './Components/cuentas/form-cuenta/form-cuenta.component';
import { NotificacionComponent } from './Components/General/notificacion/notificacion.component';
import { FormMovimientoComponent } from './Components/Movimientos/form-movimiento/form-movimiento.component';


@NgModule({
  declarations: [
    AppComponent,
    ClientesComponent,
    CuentasComponent,
    FormClienteComponent,
    MovimientosComponent,
    ReportesComponent,
    FormCuentaComponent,
    NotificacionComponent,
    FormMovimientoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    PrincipalModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    provideClientHydration(withEventReplay())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
