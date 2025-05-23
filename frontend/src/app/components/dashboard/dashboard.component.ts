import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { PetService } from '../../models/pet-service.model';
import { PetServiceService } from '../../services/pet-service.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class DashboardComponent implements OnInit {
  services: PetService[] = [];
  isAdmin: boolean = false;
  loading: boolean = false;
  error: string = '';

  constructor(
    private authService: AuthService,
    private petServiceService: PetServiceService,
    private router: Router
  ) {
    this.isAdmin = this.authService.hasRole('Admin');
  }

  ngOnInit(): void {
    this.loadServices();
  }

  loadServices(): void {
    this.loading = true;
    this.petServiceService.getAll().subscribe({
      next: (data) => {
        this.services = data;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Error loading services';
        this.loading = false;
        console.error('Error:', error);
      }
    });
  }

  getServiceImageUrl(serviceImg: string | undefined): string {

    if (!serviceImg) {
      // Use the default image from frontend assets with relative path
      return './assets/images/default-service.jpg';
    }
    // Use the image from backend wwwroot
    return `${environment.apiUrl}/images/${serviceImg}`;
  }

  bookService(serviceId: number): void {
    this.router.navigate(['/services', serviceId, 'book']);
  }

  deleteService(serviceId: number): void {
    if (confirm('Are you sure you want to delete this service?')) {
      this.petServiceService.delete(serviceId).subscribe({
        next: () => {
          this.loadServices(); // Reload services after deletion
        },
        error: (error) => {
          this.error = 'Error deleting service';
          console.error('Error:', error);
        }
      });
    }
  }

  onEdit(serviceId: number): void {
    this.router.navigate(['/services/edit', serviceId]);
  }

  navigateToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }

  navigateToServices(): void {
    this.router.navigate(['/services']);
  }

  navigateToAddService(): void {
    this.router.navigate(['/services/add']);
  }

  navigateToBookings(): void {
    this.router.navigate(['/bookings']);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
