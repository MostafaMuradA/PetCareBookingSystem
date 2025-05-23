import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { PetServiceListComponent } from './components/pet-service-list/pet-service-list.component';
import { PetServiceFormComponent } from './components/pet-service-form/pet-service-form.component';
import { BookingListComponent } from './components/booking-list/booking-list.component';
import { BookingFormComponent } from './components/booking-form/booking-form.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
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
  {
    path: 'bookings',
    component: BookingListComponent,
    canActivate: [authGuard],
    data: { roles: ['Admin', 'Customer'] }
  },
  {
    path: 'services/:id/book',
    component: BookingFormComponent,
    canActivate: [authGuard],
    data: { roles: ['Customer'] }
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];
