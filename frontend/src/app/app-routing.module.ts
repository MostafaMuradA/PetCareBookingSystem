import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PetServiceListComponent } from './components/pet-service-list/pet-service-list.component';
import { PetServiceFormComponent } from './components/pet-service-form/pet-service-form.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { 
    path: 'services', 
    component: PetServiceListComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Admin', 'Customer'] }
  },
  { 
    path: 'services/add', 
    component: PetServiceFormComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Admin'] }
  },
  { 
    path: 'services/edit/:id', 
    component: PetServiceFormComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Admin'] }
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', redirectTo: '/services', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { } 