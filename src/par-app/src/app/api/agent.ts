import axios, { AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import API_CONSTANTS from './apiConstants';
import { LogError } from '../utils/logger';
import { User, UserLogin } from '../models/User';
import { ApplicationUsers } from '../models/applicationUsers';
import { ApplicationRole } from '../models/applicationRole';
import { ApplicationUser, ApplicationUserEditFormValues, ApplicationUserFormValues } from '../models/applicationUser';
import { SchoolYears } from '../models/schoolYears';
import { SchoolYearDto, SchoolYearFormValues } from '../models/schoolYear';
import * as dayjs from "dayjs";
import {GroupDto, GroupFormValues} from "../models/group";
import {PagedResponse} from "../models/common/pagedResponse";
import {GroupDetailDto} from "../models/groupDetail";
import {NumberList} from "../models/numberList";

axios.defaults.baseURL = import.meta.env.VITE_REACT_APP_API_URL as string;

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

axios.interceptors.request.use((config) => {
    const token = window.localStorage.getItem('jwt');
    if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
    return config;
});

axios.interceptors.response.use(
    async (response: AxiosResponse) => {
        return response;
    },
    (error) => {
        if (axios.isCancel(error)) {
        } else if (!error.response) {
            LogError('Network error', error.message);
            toast.error('Błąd sieci');
        } else {
            LogError(error);
            if (error.response) {
                const { status } = error.response;

                switch (status) {
                    case 400:
                        toast.error('Nieprawidłowe żądanie');
                        break;
                    case 401:
                        toast.error('Brak autoryzacji');
                        break;
                    case 403:
                        toast.error('Brak dostępu');
                        break;
                    case 404:
                        toast.error('Nie odnaleziono zasobu na serwerze');
                        break;
                    case 500:
                        toast.error('Błąd serwera');
                        break;
                    default:
                        toast.error('Nieznany błąd');
                        break;
                }
            }
        }
        return Promise.reject(error);
    },
);

const requests = {
    get: <T>(url: string, signal?: AbortSignal) => axios.get<T>(url, { signal }).then(responseBody),
    getWithParams: <T>(url: string, params: URLSearchParams, signal?: AbortSignal) =>
        axios.get<T>(url, { params, signal }).then(responseBody),
    post: <T>(url: string, body: {}, signal?: AbortSignal) => axios.post<T>(url, body, { signal }).then(responseBody),
    put: <T>(url: string, body: {}, signal?: AbortSignal) => axios.put<T>(url, body, { signal }).then(responseBody),
    delete: <T>(url: string, signal?: AbortSignal) => axios.delete<T>(url, { signal }).then(responseBody),
};

const Account = {
    login: (user: UserLogin, signal?: AbortSignal) =>
        requests.post<User>(`${API_CONSTANTS.AUTHORIZATION}/login`, user, signal),
};

const User = {
    details: (id: string, signal?: AbortSignal) =>
        requests.get<ApplicationUser>(`${API_CONSTANTS.USERS}/${id}`, signal),
    list: (params: URLSearchParams, signal?: AbortSignal) =>
        requests.getWithParams<ApplicationUsers>(API_CONSTANTS.USERS, params, signal),
    listOptions: () => requests.get<NumberList[]>(`${API_CONSTANTS.USERS}/options`),
    exists: (email: string, signal?: AbortSignal) =>
        requests.get<boolean>(`${API_CONSTANTS.USERS}/exists/${email}`, signal),
    create: (user: ApplicationUserFormValues, signal?: AbortSignal) =>
        axios.post<string>(API_CONSTANTS.USERS, user, { signal }),
    update: (userEditFormValues: ApplicationUserEditFormValues, signal?: AbortSignal) =>
        requests.put<void>(`${API_CONSTANTS.USERS}/${userEditFormValues.id}`, userEditFormValues, signal),
};

const Role = {
    list: (signal?: AbortSignal) => requests.get<ApplicationRole[]>(API_CONSTANTS.ROLES, signal),
};

const SchoolYear = {
    list: (params: URLSearchParams, signal?: AbortSignal) =>
        requests.getWithParams<SchoolYears>(API_CONSTANTS.SCHOOL_YEARS, params, signal),
    listOptions: () => requests.get<NumberList[]>(`${API_CONSTANTS.SCHOOL_YEARS}/options`),
    details: (id: number, signal?: AbortSignal) =>
        requests.get<SchoolYearDto>(`${API_CONSTANTS.SCHOOL_YEARS}/${id}`, signal),
    create: (schoolYear: SchoolYearFormValues, signal?: AbortSignal) => {
        const payload = {
            ...schoolYear,
            startDate: dayjs(schoolYear.startDate).format('YYYY-MM-DD'),
            endDate: dayjs(schoolYear.endDate).format('YYYY-MM-DD'),
        };
        return axios.post<string>(API_CONSTANTS.SCHOOL_YEARS, payload, { signal });
    },
    update: (schoolYear: SchoolYearFormValues, signal?: AbortSignal) => {
        const payload = {
            ...schoolYear,
            startDate: dayjs(schoolYear.startDate).format('YYYY-MM-DD'),
            endDate: dayjs(schoolYear.endDate).format('YYYY-MM-DD'),
        };
        return requests.put<void>(`${API_CONSTANTS.SCHOOL_YEARS}/${schoolYear.id}`, payload, signal);
    },
};

const Group = {
    list: (params: URLSearchParams, signal?: AbortSignal) => requests.getWithParams<PagedResponse<GroupDetailDto>>(`${API_CONSTANTS.GROUPS}/details`, params, signal),
    listOptions: () => requests.get<NumberList[]>(`${API_CONSTANTS.GROUPS}/options`),
    details: (id: number, signal?: AbortSignal) => requests.get<GroupDto>(`${API_CONSTANTS.GROUPS}/${id}`, signal),
    create: (group: GroupFormValues, signal?: AbortSignal) =>
        axios.post<string>(API_CONSTANTS.GROUPS, group, { signal }),
    update: (group: GroupFormValues, signal?: AbortSignal) =>
        requests.put<void>(`${API_CONSTANTS.GROUPS}/${group.id}`, group, signal),
}

const agent = {
    Account,
    User,
    Role,
    SchoolYear,
    Group,
};
export default agent;
