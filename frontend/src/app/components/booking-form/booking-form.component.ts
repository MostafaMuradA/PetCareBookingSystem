import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { BookingService } from '../../services/booking.service';
import { CreateBooking } from '../../models/booking.model';

@Component({
  selector: 'app-booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.scss'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class BookingFormComponent implements OnInit {
  serviceId!: number;
  form: FormGroup;
  loading = false;
  error = '';
  minDate: string;

  constructor(
    private fb: FormBuilder,
    private bookingService: BookingService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.minDate = new Date().toISOString().split('T')[0];
    this.form = this.fb.group({
      bookingDate: ['', [Validators.required]],
      bookingTime: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    // Get the service ID from route parameters
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.serviceId = +id; // Convert string to number
    } else {
      this.error = 'Service ID not found';
      this.router.navigate(['/dashboard']);
    }
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.loading = true;
    const formValue = this.form.value;
    const bookingDate = new Date(formValue.bookingDate);
    const [hours, minutes] = formValue.bookingTime.split(':');
    bookingDate.setHours(parseInt(hours), parseInt(minutes));

    const booking: CreateBooking = {
      petServiceId: this.serviceId,
      bookingDate: bookingDate
    };

    this.bookingService.createBooking(booking).subscribe({
      next: () => {
        this.router.navigate(['/bookings']);
      },
      error: (err) => {
        this.error = 'Error creating booking';
        this.loading = false;
        console.error('Error:', err);
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/dashboard']);
  }
}
