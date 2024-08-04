import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccessPerWebserverComponent } from './access-per-webserver/access-per-webserver.component';
import { SuccessPerUriComponent } from './success-per-uri/success-per-uri.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  { path: '', redirectTo: '/web-server', pathMatch: 'full' },
  { path: 'web-server', component: AccessPerWebserverComponent },
  { path: 'success-uri', component: SuccessPerUriComponent },
  { path: '**', redirectTo: '/home' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
