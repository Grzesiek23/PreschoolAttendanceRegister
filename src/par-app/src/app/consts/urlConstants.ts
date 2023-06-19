export const URL_CONSTANTS = {
    HOME: '/home',
    NOT_FOUND: '/not-found',
    GROUPS: '/groups',
    PRESCHOOLERS: '/preschoolers',

    USERS: '/users',
    USERS_DETAILS: (id: number | string) => `/users/${id}`,
    USERS_EDIT: (id: string) => `/users/${id}/edit`,
    USERS_EDIT_EMAIL: (id: string) => `/users/${id}/edit-email`,
    USERS_EDIT_PASSWORD: (id: string) => `/users/${id}/edit-password`,
};
