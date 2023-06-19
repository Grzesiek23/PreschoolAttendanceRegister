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
    lastName: string = '';
    email: string = '';
    phoneNumber: string = '';
    role: string = '';
    password: string = '';
    confirmPassword: string = '';
    sendEmail: boolean = false;

    constructor(applicationUser?: ApplicationUser) {
        if (applicationUser) {
            this.id = applicationUser.id;
            this.firstName = applicationUser.firstName ?? '';
            this.lastName = applicationUser.lastName ?? '';
            this.email = applicationUser.email ?? '';
            this.phoneNumber = applicationUser.phoneNumber ?? '';
            this.role = applicationUser.role?.name ?? '';
            this.password = '';
            this.confirmPassword = '';
            this.sendEmail = false;
        }
    }
}

export class ApplicationUserEditFormValues {
    id: string = '';
    firstName: string = '';
    lastName: string = '';
    email: string = '';
    phoneNumber: string = '';
    roleId: string = '';

    constructor(applicationUser?: ApplicationUser) {
        if (applicationUser) {
            this.id = applicationUser.id ?? '';
            this.firstName = applicationUser.firstName ?? '';
            this.lastName = applicationUser.lastName ?? '';
            this.email = applicationUser.email ?? '';
            this.phoneNumber = applicationUser.phoneNumber ?? '';
            this.roleId = applicationUser.role?.id ?? '';
        }
    }
}