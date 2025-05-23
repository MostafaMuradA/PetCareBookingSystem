import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProfileService } from '../../services/profile.service';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { UpdateProfileRequest } from '../../models/profile.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;
  passwordForm: FormGroup;
  loading = false;
  changingPassword = false;

  constructor(
    private fb: FormBuilder,
    private profileService: ProfileService,
    private toastr: ToastrService
  ) {
    this.profileForm = this.fb.group({
      userName: ['', [Validators.minLength(3)]],
      phoneNumber: ['', [Validators.pattern('^(010|011|012|015)\\d{8}$')]],
      fullName: ['']
    });

    this.passwordForm = this.fb.group({
      currentPassword: ['', [Validators.required]],
      newPassword: ['', [
        Validators.required,
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{8,}$')
      ]],
      confirmNewPassword: ['', [Validators.required]]
    }, { validator: this.passwordMatchValidator });
  }

  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile(): void {
    this.loading = true;
    this.profileService.getProfile()
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: (profile) => {
          this.profileForm.patchValue({
            userName: profile.userName,
            phoneNumber: profile.phoneNumber,
            fullName: profile.fullName
          });
        },
        error: (error) => {
          this.toastr.error('Failed to load profile');
          console.error('Error loading profile:', error);
        }
      });
  }

  onUpdateProfile(): void {
    if (this.profileForm.invalid) {
      return;
    }

    const formValue = this.profileForm.value;
    const updateData: UpdateProfileRequest = Object.keys(formValue).reduce((acc: UpdateProfileRequest, key: string) => {
      if (formValue[key]) {
        acc[key as keyof UpdateProfileRequest] = formValue[key];
      }
      return acc;
    }, {});

    if (Object.keys(updateData).length === 0) {
      this.toastr.info('No changes to update');
      return;
    }

    this.loading = true;
    this.profileService.updateProfile(updateData)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: () => {
          this.toastr.success('Profile updated successfully');
          this.loadProfile();
        },
        error: (error) => {
          this.toastr.error(error.error?.message || 'Failed to update profile');
          console.error('Error updating profile:', error);
        }
      });
  }

  onChangePassword(): void {
    if (this.passwordForm.invalid) {
      return;
    }

    this.changingPassword = true;
    this.profileService.changePassword(this.passwordForm.value)
      .pipe(finalize(() => this.changingPassword = false))
      .subscribe({
        next: () => {
          this.toastr.success('Password changed successfully');
          this.passwordForm.reset();
        },
        error: (error) => {
          this.toastr.error(error.error?.message || 'Failed to change password');
          console.error('Error changing password:', error);
        }
      });
  }

  private passwordMatchValidator(g: FormGroup) {
    return g.get('newPassword')?.value === g.get('confirmNewPassword')?.value
      ? null
      : { mismatch: true };
  }
}
