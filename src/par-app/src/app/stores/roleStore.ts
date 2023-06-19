import {makeAutoObservable} from "mobx";
import {ApplicationRole} from "../models/applicationRole";
import {store} from "./store";
import agent from "../api/agent";
import {LogError} from "../utils/logger";

export default class RoleStore {
    constructor() {
        makeAutoObservable(this);
    }

    roles: ApplicationRole[] | undefined = undefined;
    
    setRoles = (rolesList: ApplicationRole[]) => {
        this.roles = rolesList;
    }

    loadRoles = async (): Promise<void> => {
        try {
            store.commonStore.setLoadingIndicator(true);
            const roles = await agent.Role.list();
            this.setRoles(roles);
        } catch (error) {
            LogError(error);
        } finally {
            store.commonStore.setLoadingIndicator(false);
        }
    };
}
    