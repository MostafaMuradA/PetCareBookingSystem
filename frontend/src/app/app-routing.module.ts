import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PetServiceListComponent } from './components/pet-service-list/pet-service-list.component';
import { PetServiceFormComponent } from './components/pet-service-form/pet-service-form.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { authGuard } from './guards/auth.guard';
import { ProfileComponent } from './components/profile/profile.component';

const routes: Routes = [
  { 
    path: 'dashboard', 
    component: DashboardComponent,
    canActivate: [authGuard],
    data: { roles: ['Admin', 'Customer'] }
  },
  { 
    path: 'services', 
    component: PetServiceListComponent,
    canActivate: [authGuard],
    data: { roles: ['Admin', 'Customer'] }
  },
  { 
    path: 'services/add', 
    component: PetServiceFormComponent,
    canActivate: [authGuard],
    data: { roles: ['Admin'] }
  },
  { 
    path: 'services/edit/:id', 
    component: PetServiceFormComponent,
    canActivate: [authGuard],
    data: { roles: ['Admin'] }
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [authGuard] },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { } 