import {makeAutoObservable, runInAction} from "mobx";
import {GroupDto, GroupFormValues} from "../models/group";
import {GridPaginationModel, GridSortModel} from "@mui/x-data-grid";
import {GRID_CONSTANTS} from "../consts/gridConstants";
import {LogError} from "../utils/logger";
import agent from "../api/agent";
import {PagedResponse} from "../models/common/pagedResponse";
import {GroupDetailDto} from "../models/groupDetail";
import {store} from "./store";

export default class GroupStore{
    constructor() {
        makeAutoObservable(this);
    }
    
    group: GroupDto | undefined = undefined;
    groups: PagedResponse<GroupDetailDto> | undefined = undefined;
    pagination: GridPaginationModel = { page: 0, pageSize: GRID_CONSTANTS.DEFAULT_PAGE_SIZE };
    sortModel: GridSortModel | undefined = [{ field: 'id', sort: 'asc' }];
    abortController: AbortController | undefined = undefined;
    createdGroupId: number = 0;

    createOrReplaceAbortController = () => {
        if (this.abortController) {
            this.abortController.abort();
        }
        this.abortController = new AbortController();
    };
    
    setGroup = (group: GroupDto) => {
        this.group = group;
    }
    
    clearGroup = () => {
        this.group = undefined;
    }
    
    setGroups = (groups: PagedResponse<GroupDetailDto>) => {
        this.groups = groups;
    }
    
    loadGroup = async (id: number): Promise<void> => {
        this.createOrReplaceAbortController();
        try {
            const group = await agent.Group.details(id, this.abortController!.signal);
            if (group) {
                this.setGroup(group);
            }
        } catch (error) {
            LogError(error);
        }
    }
    
    loadGroups = async (): Promise<void> => {
        try {
            this.createOrReplaceAbortController();
            const groups = await agent.Group.list(this.axiosParams, this.abortController!.signal);
            if (groups) {
                this.setGroups(groups);
            }
        } catch (error) {
            LogError(error);
        }
    }

    createGroup = async (groupFormValues: GroupFormValues): Promise<boolean> => {
        try {
            this.createOrReplaceAbortController();
            store.commonStore.setLoadingIndicator(true);
            const response = await agent.Group.create(groupFormValues, this.abortController!.signal);
            if (response.status === 201 && response.data !== null) {
                runInAction(() => {
                    this.createdGroupId = parseInt(response.data);
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

    updateGroup = async (groupFormValues: GroupFormValues): Promise<boolean> => {
        try {
            this.createOrReplaceAbortController();
            store.commonStore.setLoadingIndicator(true);
            await agent.Group.update(groupFormValues, this.abortController!.signal);
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

        return params;
    }

    handlePageChange = async (paginationModel: GridPaginationModel): Promise<void> => {
        runInAction(() => {
            this.pagination = paginationModel;
        });
        await this.loadGroups();
    };

    handleSortModelChange = async (sortModel: GridSortModel): Promise<void> => {
        runInAction(() => {
            this.sortModel = sortModel;
        });
        await this.loadGroups();
    };
}