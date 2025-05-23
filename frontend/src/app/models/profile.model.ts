export interface UpdateProfileRequest {
    userName?: string;
    phoneNumber?: string;
    fullName?: string;
}

export interface ChangePasswordRequest {
    currentPassword: string;
    newPassword: string;
    confirmNewPassword: string;
}

export interface ProfileResponse {
    id: string;
    userName: string;
    email: string;
    fullName: string;
    phoneNumber: string;
} 