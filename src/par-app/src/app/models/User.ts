export interface UserLogin {
    email: string;
    password: string;
}

export interface User {
    id: number;
    email: string;
    token: string;
    fullName: string;
}