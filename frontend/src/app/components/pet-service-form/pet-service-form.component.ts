import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PetService } from '../../models/pet-service.model';
import { PetServiceService } from '../../services/pet-service.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-pet-service-form',
  templateUrl: './pet-service-form.component.html',
  styleUrls: ['./pet-service-form.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, NgIf]
})
export class PetServiceFormComponent implements OnInit {
  form: FormGroup;
  isEditing = false;
  loading = false;
  error = '';
  serviceId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private petServiceService: PetServiceService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.form = this.fb.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      price: ['', [Validators.required, Validators.min(0)]],
      duration: ['', [Validators.required, Validators.min(1)]]
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditing = true;
      this.serviceId = +id;
      this.loadService(this.serviceId);
    }
  }

  loadService(id: number): void {
    this.loading = true;
    this.petServiceService.getById(id).subscribe({  
      next: (service) => {
        this.form.patchValue(service);
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error loading service';
        this.loading = false;
        console.error('Error:', err);
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.loading = true;
    const service: PetService = {
      ...this.form.value,
      id: this.serviceId || 0
    };

    if (!this.isEditing) {
      service.serviceImg = 'default-service.jpg';
    }

    if (this.isEditing && this.serviceId) {
      this.petServiceService.update(this.serviceId, service).subscribe({
        next: () => {
          this.router.navigate(['/services']);
        },
        error: (err) => {
          this.error = 'Error updating service';
          this.loading = false;
          console.error('Error:', err);
        }
      });
    } else {
      this.petServiceService.add(service).subscribe({
        next: () => {
          this.router.navigate(['/services']);
        },
        error: (err) => {
          this.error = 'Error adding service';
          this.loading = false;
          console.error('Error:', err);
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/services']);
  }
}
