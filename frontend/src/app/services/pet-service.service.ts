import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PetService } from '../models/pet-service.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PetServiceService {
  private apiUrl = `${environment.apiUrl}/api/PetServices`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<PetService[]> {
    return this.http.get<PetService[]>(this.apiUrl, { withCredentials: true });
  }

  getById(id: number): Observable<PetService> {
    return this.http.get<PetService>(`${this.apiUrl}/${id}`, { withCredentials: true });
  }

  add(service: PetService): Observable<PetService> {
    return this.http.post<PetService>(this.apiUrl, service, { withCredentials: true });
  }

  update(id: number, service: PetService): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, service, { withCredentials: true });
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`, { withCredentials: true });
  }
} 