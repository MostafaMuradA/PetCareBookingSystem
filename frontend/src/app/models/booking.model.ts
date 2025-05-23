export interface Booking {
    id: number;
    userId: string;
    userName: string;
    petServiceId: number;
    serviceName: string;
    bookingDate: Date;
    status: string;
    price: number;
}

export interface CreateBooking {
    petServiceId: number;
    bookingDate: Date;
}

export interface UpdateBookingStatus {
    status: string;
} 