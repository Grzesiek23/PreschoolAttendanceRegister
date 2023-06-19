import { createContext, useContext } from 'react';
import CommonStore from './commonStore';
import AccountStore from './accountStore';
import UserStore from './userStore';
import RoleStore from './roleStore';
import SchoolYearStore from "./schoolYearStore";

interface Store {
    commonStore: CommonStore;
    accountStore: AccountStore;
    userStore: UserStore;
    roleStore: RoleStore;
    schoolYearStore: SchoolYearStore;
}

export const store: Store = {
    commonStore: new CommonStore(),
    accountStore: new AccountStore(),
    userStore: new UserStore(),
    roleStore: new RoleStore(),
    schoolYearStore: new SchoolYearStore()
};

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}
