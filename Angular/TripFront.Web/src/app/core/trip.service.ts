import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { CreateTripRequest, PagedResult, Trip } from './api.models';

const apiUrl = 'http://localhost:5146/api';

@Injectable({ providedIn: 'root' })
export class TripService {
  private readonly http = inject(HttpClient);

  list(pageNumber = 1, pageSize = 50) {
    const params = new HttpParams().set('pageNumber', pageNumber).set('pageSize', pageSize);
    return this.http.get<PagedResult<Trip>>(`${apiUrl}/trips`, { params, withCredentials: true });
  }

  create(request: CreateTripRequest) {
    return this.http.post<Trip>(`${apiUrl}/trips`, request, { withCredentials: true });
  }

  delete(id: number) {
    return this.http.delete<void>(`${apiUrl}/trips/${id}`, { withCredentials: true });
  }
}
