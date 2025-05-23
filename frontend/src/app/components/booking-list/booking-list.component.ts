import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BookingService } from '../../services/booking.service';
import { AuthService } from '../../services/auth.service';
import { Booking } from '../../models/booking.model';

@Component({
  selector: 'app-booking-list',
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.scss'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class BookingListComponent implements OnInit {
  bookings: Booking[] = [];
  isAdmin: boolean = false;
  loading: boolean = false;
  error: string = '';

  constructor(
    private bookingService: BookingService,
    private authService: AuthService
  ) {
    this.isAdmin = this.authService.hasRole('Admin');
  }

  ngOnInit(): void {
    this.loadBookings();
  }

  loadBookings(): void {
    debugger
    this.loading = true;
    const bookingsObservable =
      this.bookingService.getAllBookings()

    bookingsObservable.subscribe({
      next: (data) => {
        this.bookings = data;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Error loading bookings';
        this.loading = false;
        console.error('Error:', error);
      }
    });
  }

  updateStatus(bookingId: number, newStatus: string): void {
    this.bookingService.updateBookingStatus(bookingId, { status: newStatus }).subscribe({
      next: () => {
        this.loadBookings();
      },
      error: (error) => {
        this.error = 'Error updating booking status';
        console.error('Error:', error);
      }
    });
  }

  deleteBooking(bookingId: number): void {
    if (confirm('Are you sure you want to cancel this booking?')) {
      this.bookingService.deleteBooking(bookingId).subscribe({
        next: () => {
          this.loadBookings();
        },
        error: (error) => {
          this.error = 'Error canceling booking';
          console.error('Error:', error);
        }
      });
    }
  }
}
