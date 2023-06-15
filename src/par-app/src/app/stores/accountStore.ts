import { makeAutoObservable } from 'mobx';
import { User, UserLogin } from "../models/User";
import jwt_decode from 'jwt-decode';
import { store } from './store';
import agent from '../api/agent';

type DecodedToken = {
    id: number;
    email: string;
    firstName: string;
    lastName: string;
    role: string[];
    exp: number;
};

export default class AccountStore {
    constructor() {
        makeAutoObservable(this);

        const token = window.localStorage.getItem('jwt');
        if (token && !this.isTokenExpired(token)) {
            const decoded: DecodedToken = jwt_decode(token);

            this.user = {
                id: decoded.id,
                email: decoded.email,
                fullName: decoded.firstName + ' ' + decoded.lastName,
                token: token
            };
        }
    }

    user: User | null = null;
    abortController: AbortController | undefined = undefined;

    createOrReplaceAbortController = () => {
        if (this.abortController) {
            this.abortController.abort();
        }
        this.abortController = new AbortController();
    };

    getRoles = () => {
        const token = window.localStorage.getItem('jwt');
        if (!token) return [];

        const decoded: DecodedToken = jwt_decode(token);
        return decoded.role ?? [];
    }

    isLoggedIn = () => {
        const token = window.localStorage.getItem('jwt');
        return !!(this.user && token && !this.isTokenExpired(token));
    };

    isTokenExpired = (token: string) => {
        const decoded: DecodedToken = jwt_decode(token);
        const currentTime = Math.floor(Date.now() / 1000);

        return decoded.exp < currentTime;
    };

    setUser = (user: User | null) => {
        this.user = user;
    };

    hasRole = (role: string) => {
        return this.getRoles().includes(role);
    };

    loginUser = async (credentials: UserLogin) => {
        try {
            this.createOrReplaceAbortController();

            const user = await agent.Account.login(credentials, this.abortController!.signal);
            store.commonStore.setToken(user.token);
            this.setUser(user);
        } catch (error) {
            console.log(error);
            throw error;
        }
    };

    logoutUser = () => {
        store.commonStore.setToken(null);
        this.setUser(null);
        if (this.abortController) {
            this.abortController.abort();
            this.abortController = undefined;
        }
    };
}
