import { makeAutoObservable } from 'mobx';
import {User, UserLogin} from "../models/User";
import jwt_decode from 'jwt-decode';
import { store } from './store';
import agent from '../api/agent';

type DecodedToken = {
    role: string[];
    exp: number;
};
export default class AccountStore {
    constructor() {
        makeAutoObservable(this);
        const token = window.localStorage.getItem('jwt');
        if (token) {
            this.setRoles(token);
        }
    }

    user: User | null = null;
    abortController: AbortController | undefined = undefined;
    roles: string[] = [];

    createOrReplaceAbortController = () => {
        if (this.abortController) {
            this.abortController.abort();
        }
        this.abortController = new AbortController();
    };

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

        if (user && user?.token !== null) {
            this.setRoles(user.token);
        }
    };

    setRoles = (token: string) => {
        const decoded: DecodedToken = jwt_decode(token);
        if (decoded && decoded.role) {
            this.roles = decoded.role;
            window.localStorage.setItem('userRoles', JSON.stringify(decoded.role));
        } else {
            this.roles = [];
            window.localStorage.removeItem('userRoles');
        }
    };

    hasRole = (role: string) => {
        return this.roles.includes(role);
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
        window.localStorage.removeItem('userRoles');
    };
}
