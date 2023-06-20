import { makeAutoObservable, runInAction } from 'mobx';
import { GridPaginationModel, GridSortModel } from '@mui/x-data-grid';
import { GRID_CONSTANTS } from '../consts/gridConstants';
import { store } from './store';
import agent from '../api/agent';
import { LogError } from '../utils/logger';
import { ApplicationUsers } from '../models/applicationUsers';
import {ApplicationUser, ApplicationUserEditFormValues, ApplicationUserFormValues} from '../models/applicationUser';
import {NumberList} from "../models/numberList";

export default class UserStore {
    constructor() {
        makeAutoObservable(this);
    }

    user: ApplicationUser | undefined = undefined;
    users: ApplicationUsers | undefined = undefined;
    userOptionList: NumberList[] = [];
    pagination: GridPaginationModel = { page: 0, pageSize: GRID_CONSTANTS.DEFAULT_PAGE_SIZE };
    sortModel: GridSortModel | undefined = [{ field: 'id', sort: 'asc' }];
    abortController: AbortController | undefined = undefined;
    filterFirstName: string = '';
    filterLastName: string = '';
    filterEmail: string = '';
    createdUserId: string = '';

    createOrReplaceAbortController = () => {
        if (this.abortController) {
            this.abortController.abort();
        }
        this.abortController = new AbortController();
    };

    setUser = (user: ApplicationUser) => {
        this.user = user;
    };

    clearUser = () => {
        this.user = undefined;
    };

    setUsers = (user: ApplicationUsers) => {
        this.users = user;
    };

    setUserOptionList = (userOptionList: NumberList[]) => {
        this.userOptionList = userOptionList;
    };
    
    loadUser = async (id: string): Promise<void> => {
        this.createOrReplaceAbortController();
        store.commonStore.setLoadingIndicator(true);
        try {
            const user = await agent.User.details(id, this.abortController!.signal);
            if (user) {
                this.setUser(user);
            }
        } catch (error) {
            LogError(error);
        } finally {
            store.commonStore.setLoadingIndicator(false);
        }
    };
    
    loadUsers = async (): Promise<void> => {
        try {
            this.createOrReplaceAbortController();

            store.commonStore.setLoadingIndicator(true);
            const users = await agent.User.list(this.axiosParams, this.abortController!.signal);
            this.setUsers(users);
        } catch (error) {
            LogError(error);
        } finally {
            store.commonStore.setLoadingIndicator(false);
        }
    };
    
    loadUsersOptionList = async (): Promise<void> => {
        try {
            store.commonStore.setLoadingIndicator(true);
            const users = await agent.User.listOptions();
            this.setUserOptionList(users);
        } catch (error) {
            LogError(error);
        } finally {
            store.commonStore.setLoadingIndicator(false);
        }
    };

    exists = async (email: string): Promise<boolean> => {
        try {
            return await agent.User.exists(email);
        } catch (error) {
            LogError(error);
            return false;
        }
    };

    createUser = async (userFormValues: ApplicationUserFormValues): Promise<boolean> => {
        try {
            this.createOrReplaceAbortController();

            store.commonStore.setLoadingIndicator(true);
            const response = await agent.User.create(userFormValues, this.abortController!.signal);
            if (response.status === 201 && response.data !== null) {
                runInAction(() => {
                    this.createdUserId = response.data;
                });
                return true;
            }
            return false;
        } catch (error) {
            LogError(error);
            return false;
        } finally {
            store.commonStore.setLoadingIndicator(false);
        }
    };

    updateUser = async (user: ApplicationUserEditFormValues): Promise<boolean> => {
        try {
            this.createOrReplaceAbortController();
            store.commonStore.setLoadingIndicator(true);

            await agent.User.update(user, this.abortController!.signal);
            return true;
        } catch (error) {
            LogError(error);
            return false;
        } finally {
            store.commonStore.setLoadingIndicator(false);
        }
    };
    
    get axiosParams() {
        const params = new URLSearchParams();
        params.append('page', this.pagination.page.toString());
        params.append('pageSize', this.pagination.pageSize.toString());
        params.append('sortField', this.sortModel?.[0]?.field || 'id');
        params.append('sortOrder', this.sortModel?.[0]?.sort || 'asc');

        if (this.filterFirstName) {
            params.append('firstName', this.filterFirstName);
        }
        if (this.filterLastName) {
            params.append('lastName', this.filterLastName);
        }
        if (this.filterEmail) {
            params.append('email', this.filterEmail);
        }
        return params;
    }

    setFilterFirstName = async (firstName: string) => {
        this.filterFirstName = firstName;
        await this.loadUsers();
    };

    setFilterLastName = async (surname: string) => {
        this.filterLastName = surname;
        await this.loadUsers();
    };

    setFilterEmail = async (email: string) => {
        this.filterEmail = email;
        await this.loadUsers();
    };

    resetFilters = async () => {
        this.filterFirstName = '';
        this.filterLastName = '';
        this.filterEmail = '';
        await this.loadUsers();
    };

    handlePageChange = async (paginationModel: GridPaginationModel): Promise<void> => {
        runInAction(() => {
            this.pagination = paginationModel;
        });
        await this.loadUsers();
    };

    handleSortModelChange = async (sortModel: GridSortModel): Promise<void> => {
        runInAction(() => {
            this.sortModel = sortModel;
        });
        await this.loadUsers();
    };
}
