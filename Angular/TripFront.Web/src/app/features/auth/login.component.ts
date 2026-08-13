import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './auth.component.css',
})
export class LoginComponent {
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);

  protected username = '';
  protected password = '';
  protected readonly submitting = signal(false);
  protected readonly error = signal('');

  submit(): void {
    this.submitting.set(true);
    this.error.set('');
    this.auth.login({ userName: this.username, password: this.password }).subscribe({
      next: () => this.router.navigateByUrl('/trips'),
      error: (response) => {
        this.error.set(response.error?.errors?.join(' ') || 'نام کاربری یا رمز عبور نادرست است.');
        this.submitting.set(false);
      },
      complete: () => this.submitting.set(false),
    });
  }
}
