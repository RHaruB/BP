import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientesComponent } from './Components/clientes/clientes.component';
import { CuentasComponent } from './Components/cuentas/cuentas.component';
import { FormClienteComponent } from './Components/clientes/form-cliente/form-cliente.component';
import { MovimientosComponent } from './Components/movimientos/movimientos.component';
import { ReportesComponent } from './Components/reportes/reportes.component';
import { FormCuentaComponent } from './Components/cuentas/form-cuenta/form-cuenta.component';
import { FormMovimientoComponent } from './Components/movimientos/form-movimiento/form-movimiento.component';

const routes: Routes = [
  // {
  //   path: 'clientes',
  //   component: ClientesComponent
  // },
 
  
   {path:"",redirectTo : "clientes", pathMatch: "full"},

  { path: 'clientes', component: ClientesComponent },
  { path: 'formularioclientes', component: FormClienteComponent },
  { path: 'clientes/editar/:id', component: FormClienteComponent },
  { path: 'movimientos', component: MovimientosComponent },
  { path: 'reportes', component: ReportesComponent },
  { path: 'formularioCuentas', component: FormCuentaComponent },
  { path: 'formularioMovimientos', component: FormMovimientoComponent },
  { path: 'cuentas', component: CuentasComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
