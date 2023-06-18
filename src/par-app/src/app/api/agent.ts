import axios, { AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import API_CONSTANTS from './apiConstants';
import { LogError } from '../utils/logger';
import { User, UserLogin } from '../models/User';
import { ApplicationUsers } from '../models/applicationUsers';

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
    list: (params: URLSearchParams, signal?: AbortSignal) =>
        requests.getWithParams<ApplicationUsers>(API_CONSTANTS.USERS, params, signal),
};

const agent = {
    Account,
    User,
};
export default agent;
