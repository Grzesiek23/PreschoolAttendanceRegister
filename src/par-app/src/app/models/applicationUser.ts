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
    roles: ApplicationRole | null;
}
