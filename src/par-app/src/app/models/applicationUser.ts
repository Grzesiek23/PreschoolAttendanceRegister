import { ApplicationRole } from './applicationRole';

export interface ApplicationUser {
    id: string;
    firstName: string | null;
    lastName: string | null;
    email: string | null;
    phoneNumber: string | null;
    createDate: Date | null;
    lastActivityDate: Date | null;
    lockoutEnabled: boolean;
    lockoutEnd: Date | null;
    lockoutExpired: boolean;
    role: ApplicationRole | null;
}

export class ApplicationUserFormValues {
    id: string = '';
    firstName: string = '';
    surname: string = '';
    email: string = '';
    phoneNumber: string = '';
    brokerId: number = 0;
    branchId: number = 0;
    role: string = '';
    password: string = '';
    confirmPassword: string = '';
    sendEmail: boolean = false;

    constructor(applicationUser?: ApplicationUser) {
        if (applicationUser) {
            this.id = applicationUser.id;
            this.firstName = applicationUser.firstName ?? '';
            this.surname = applicationUser.lastName ?? '';
            this.email = applicationUser.email ?? '';
            this.phoneNumber = applicationUser.phoneNumber ?? '';
            this.role = applicationUser.role?.name ?? '';
            this.password = '';
            this.confirmPassword = '';
            this.sendEmail = false;
        }
    }
}