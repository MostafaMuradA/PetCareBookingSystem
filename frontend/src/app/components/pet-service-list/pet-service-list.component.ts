import { Component, OnInit } from '@angular/core';
import { PetService } from '../../models/pet-service.model';
import { PetServiceService } from '../../services/pet-service.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-pet-service-list',
  templateUrl: './pet-service-list.component.html',
  styleUrls: ['./pet-service-form.component.scss'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class PetServiceListComponent implements OnInit {
  services: PetService[] = [];
  loading = false;
  error = '';

  constructor(
    private petServiceService: PetServiceService,
    private router: Router
  ) { }

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

  onEdit(id: number): void {
    this.router.navigate(['/services/edit', id]);
  }

  onDelete(id: number): void {
    if (confirm('Are you sure you want to delete this service?')) {
      this.petServiceService.delete(id).subscribe({
        next: () => {
          this.loadServices();
        },
        error: (error) => {
          this.error = 'Error deleting service';
          this.loading = false;
          console.error('Error:', error);
        }
      });
    }
  }
}
