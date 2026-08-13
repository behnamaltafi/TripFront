import { DatePipe, DecimalPipe } from '@angular/common';
import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Trip } from '../../core/api.models';
import { TripService } from '../../core/trip.service';

@Component({
  selector: 'app-trips',
  imports: [DatePipe, DecimalPipe, FormsModule],
  templateUrl: './trips.component.html',
  styleUrl: './trips.component.css',
})
export class TripsComponent implements OnInit {
  private readonly tripsApi = inject(TripService);

  protected readonly trips = signal<Trip[]>([]);
  protected readonly loading = signal(true);
  protected readonly saving = signal(false);
  protected readonly error = signal('');
  protected showCreateForm = false;
  protected model = this.emptyTrip();

  ngOnInit(): void {
    this.loadTrips();
  }

  loadTrips(): void {
    this.loading.set(true);
    this.error.set('');
    this.tripsApi.list().subscribe({
      next: (result) => this.trips.set(result.items || []),
      error: () => this.error.set('سفرها بارگذاری نشدند. لطفاً دوباره تلاش کنید.'),
      complete: () => this.loading.set(false),
    });
  }

  openCreateForm(): void {
    this.model = this.emptyTrip();
    this.showCreateForm = true;
  }

  createTrip(): void {
    this.saving.set(true);
    this.error.set('');
    this.tripsApi.create({
      ...this.model,
      startDate: new Date(this.model.startDate).toISOString(),
      endDate: this.model.endDate ? new Date(this.model.endDate).toISOString() : null,
    }).subscribe({
      next: (trip) => {
        this.trips.update((items) => [trip, ...items]);
        this.showCreateForm = false;
      },
      error: (response) => this.error.set(response.error?.error || 'ثبت سفر انجام نشد.'),
      complete: () => this.saving.set(false),
    });
  }

  deleteTrip(trip: Trip): void {
    if (!confirm(`«${trip.title}» حذف شود؟`)) {
      return;
    }

    this.tripsApi.delete(trip.id).subscribe({
      next: () => this.trips.update((items) => items.filter((item) => item.id !== trip.id)),
      error: () => this.error.set('حذف سفر انجام نشد.'),
    });
  }

  private emptyTrip() {
    const today = new Date().toISOString().slice(0, 10);
    return { title: '', description: '', memberCount: 1, startDate: today, endDate: '' };
  }
}
