import { Component, OnInit } from '@angular/core';
import { PetService } from '../../models/pet-service.model';
import { PetServiceService } from '../../services/pet-service.service';
import { Router } from '@angular/router';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-pet-service-list',
  templateUrl: './pet-service-list.component.html',
  styleUrls: ['./pet-service-form.component.scss'],
  imports:[NgIf,FormsModule,ReactiveFormsModule,NgFor]
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
          console.error('Error:', error);
        }
      });
    }
  }
}
