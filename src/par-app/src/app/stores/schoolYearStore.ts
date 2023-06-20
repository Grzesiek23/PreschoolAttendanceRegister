import { makeAutoObservable, runInAction } from 'mobx';
import { GridPaginationModel, GridSortModel } from '@mui/x-data-grid';
import { GRID_CONSTANTS } from '../consts/gridConstants';
import { store } from './store';
import agent from '../api/agent';
import { LogError } from '../utils/logger';
import {SchoolYearDto, SchoolYearFormValues} from "../models/schoolYear";
import {SchoolYears} from "../models/schoolYears";
import {NumberList} from "../models/numberList";

export default class SchoolYearStore {
    constructor() {
        makeAutoObservable(this);
    }

    schoolYear: SchoolYearDto | undefined = undefined;
    schoolYears: SchoolYears | undefined = undefined;
    schoolYearListOption: NumberList[] = [];
    pagination: GridPaginationModel = { page: 0, pageSize: GRID_CONSTANTS.DEFAULT_PAGE_SIZE };
    sortModel: GridSortModel | undefined = [{ field: 'id', sort: 'asc' }];
    abortController: AbortController | undefined = undefined;
    createdSchoolYearId: number = 0;
    createOrReplaceAbortController = () => {
        if (this.abortController) {
            this.abortController.abort();
        }
        this.abortController = new AbortController();
    };

    setSchoolYear = (schoolYear: SchoolYearDto) => {
        this.schoolYear = schoolYear;
    };
    
    clearSchoolYear = () => {
        this.schoolYear = undefined;
    };
    
    setSchoolYears = (schoolYears: SchoolYears) => {
        this.schoolYears = schoolYears;
    };
    
    setSchoolYearListOption = (schoolYearListOption: NumberList[]) => {
        this.schoolYearListOption = schoolYearListOption;
    };

    loadSchoolYear = async (id: number): Promise<void> => {
        this.createOrReplaceAbortController();
        store.commonStore.setLoadingIndicator(true);
        try {
            const user = await agent.SchoolYear.details(id, this.abortController!.signal);
            if (user) {
                this.setSchoolYear(user);
            }
        } catch (error) {
            LogError(error);
        } finally {
            store.commonStore.setLoadingIndicator(false);
        }
    };
    
    loadSchoolYears = async (): Promise<void> => {
        try {
            this.createOrReplaceAbortController();

            store.commonStore.setLoadingIndicator(true);
            const users = await agent.SchoolYear.list(this.axiosParams, this.abortController!.signal);
            this.setSchoolYears(users);
        } catch (error) {
            LogError(error);
        } finally {
            store.commonStore.setLoadingIndicator(false);
        }
    };

    loadSchoolYearsOptionList = async (): Promise<void> => {
        try {
            this.createOrReplaceAbortController();
            const users = await agent.SchoolYear.listOptions();
            this.setSchoolYearListOption(users);
        } catch (error) {
            LogError(error);
        } finally {
            store.commonStore.setLoadingIndicator(false);
        }
    };
            
    createSchoolYear = async (schoolYearFormValues: SchoolYearFormValues): Promise<boolean> => {
        try {
            this.createOrReplaceAbortController();
            store.commonStore.setLoadingIndicator(true);
            const response = await agent.SchoolYear.create(schoolYearFormValues, this.abortController!.signal);
            if (response.status === 201 && response.data !== null) {
                runInAction(() => {
                    this.createdSchoolYearId = parseInt(response.data);
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

    updateSchoolYear = async (schoolYearFormValues: SchoolYearFormValues): Promise<boolean> => {
        try {
            this.createOrReplaceAbortController();
            store.commonStore.setLoadingIndicator(true);
            await agent.SchoolYear.update(schoolYearFormValues, this.abortController!.signal);
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
        await this.loadSchoolYears();
    };

    handleSortModelChange = async (sortModel: GridSortModel): Promise<void> => {
        runInAction(() => {
            this.sortModel = sortModel;
        });
        await this.loadSchoolYears();
    };
}
