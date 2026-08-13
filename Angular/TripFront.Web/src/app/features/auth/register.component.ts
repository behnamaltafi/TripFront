import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './auth.component.css',
})
export class RegisterComponent {
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);

  protected model = { email: '', username: '', password: '', firstName: '', lastName: '', accountNumber: '' };
  protected readonly submitting = signal(false);
  protected readonly error = signal('');

  submit(): void {
    this.submitting.set(true);
    this.error.set('');
    this.auth.register(this.model).subscribe({
      next: () => this.router.navigateByUrl('/trips'),
      error: (response) => {
        this.error.set(response.error?.errors?.join(' ') || 'ثبت‌نام انجام نشد. لطفاً اطلاعات را بررسی کنید.');
        this.submitting.set(false);
      },
      complete: () => this.submitting.set(false),
    });
  }
}
