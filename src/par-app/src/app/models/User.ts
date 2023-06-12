export interface UserLogin {
    email: string;
    password: string;
}

export interface User {
    id: string;
    email: string;
    token: string;
    firstName: string;
    lastName: number;
}