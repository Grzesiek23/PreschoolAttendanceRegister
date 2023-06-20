import {makeAutoObservable, runInAction} from "mobx";
import {GridPaginationModel, GridSortModel} from "@mui/x-data-grid";
import {GRID_CONSTANTS} from "../consts/gridConstants";
import {LogError} from "../utils/logger";
import agent from "../api/agent";
import {PagedResponse} from "../models/common/pagedResponse";
import {store} from "./store";
import {PreschoolerDto, PreschoolerFormValues} from "../models/preschooler";
import {PreschoolerDetailDto} from "../models/preschoolerDetail";

export default class PreschoolerStore{
    constructor() {
        makeAutoObservable(this);
    }
    
    preschooler: PreschoolerDto | undefined = undefined;
    preschoolers: PagedResponse<PreschoolerDetailDto> | undefined = undefined;
    pagination: GridPaginationModel = { page: 0, pageSize: GRID_CONSTANTS.DEFAULT_PAGE_SIZE };
    sortModel: GridSortModel | undefined = [{ field: 'id', sort: 'asc' }];
    abortController: AbortController | undefined = undefined;
    createdPreschoolerId: number = 0;

    createOrReplaceAbortController = () => {
        if (this.abortController) {
            this.abortController.abort();
        }
        this.abortController = new AbortController();
    };
    
    setPreschooler = (group: PreschoolerDto) => {
        this.preschooler = group;
    }
    
    clearPreschooler = () => {
        this.preschooler = undefined;
    }
    
    setPreschoolers = (preschoolers: PagedResponse<PreschoolerDetailDto>) => {
        this.preschoolers = preschoolers;
    }
    
    loadPreschooler = async (id: number): Promise<void> => {
        this.createOrReplaceAbortController();
        try {
            const preschooler = await agent.Preschooler.details(id, this.abortController!.signal);
            if (preschooler) {
                this.setPreschooler(preschooler);
            }
        } catch (error) {
            LogError(error);
        }
    }
    
    loadPreschoolers = async (): Promise<void> => {
        try {
            this.createOrReplaceAbortController();
            const preschoolers = await agent.Preschooler.list(this.axiosParams, this.abortController!.signal);
            if (preschoolers) {
                this.setPreschoolers(preschoolers);
            }
        } catch (error) {
            LogError(error);
        }
    }

    createPreschooler = async (preschoolerFormValues: PreschoolerFormValues): Promise<boolean> => {
        try {
            this.createOrReplaceAbortController();
            store.commonStore.setLoadingIndicator(true);
            const response = await agent.Preschooler.create(preschoolerFormValues, this.abortController!.signal);
            if (response.status === 201 && response.data !== null) {
                runInAction(() => {
                    this.createdPreschoolerId = parseInt(response.data);
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

    updatePreschooler = async (preschoolerFormValues: PreschoolerFormValues): Promise<boolean> => {
        try {
            this.createOrReplaceAbortController();
            store.commonStore.setLoadingIndicator(true);
            await agent.Preschooler.update(preschoolerFormValues, this.abortController!.signal);
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
        await this.loadPreschoolers();
    };

    handleSortModelChange = async (sortModel: GridSortModel): Promise<void> => {
        runInAction(() => {
            this.sortModel = sortModel;
        });
        await this.loadPreschoolers();
    };
}