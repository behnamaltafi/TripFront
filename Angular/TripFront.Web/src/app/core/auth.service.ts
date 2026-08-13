import { HttpClient } from '@angular/common/http';
import { Injectable, computed, inject, signal } from '@angular/core';
import { catchError, of, tap } from 'rxjs';
import { AuthResponse, LoginRequest, RegisterRequest } from './api.models';

const apiUrl = 'http://localhost:5146/api';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly currentUser = signal<AuthResponse | null>(null);

  readonly isAuthenticated = computed(() => this.currentUser() !== null);

  login(request: LoginRequest) {
    return this.http.post<AuthResponse>(`${apiUrl}/auth/login`, request, { withCredentials: true }).pipe(
      tap((response) => this.currentUser.set(response)),
    );
  }

  register(request: RegisterRequest) {
    return this.http.post<AuthResponse>(`${apiUrl}/auth/register`, request, { withCredentials: true }).pipe(
      tap((response) => this.currentUser.set(response)),
    );
  }

  restoreSession() {
    return this.http.get<AuthResponse>(`${apiUrl}/auth/me`, { withCredentials: true }).pipe(
      tap((response) => this.currentUser.set(response)),
      catchError(() => {
        this.currentUser.set(null);
        return of(null);
      }),
    );
  }

  logout() {
    return this.http.post<void>(`${apiUrl}/auth/logout`, {}, { withCredentials: true }).pipe(
      tap(() => this.currentUser.set(null)),
    );
  }
}
