export interface User {
    id: string;
    userName: string;
    email: string;
    role: string;
    token?: string;
}

export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
  phoneNumber:string;
    userName: string;
    email: string;
    password: string;
    confirmPassword: string;
}
