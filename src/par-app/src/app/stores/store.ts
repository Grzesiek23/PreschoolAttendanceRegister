import { createContext, useContext } from 'react';
import CommonStore from './commonStore';
import AccountStore from "./accountStore";
import UserStore from "./userStore";

interface Store {
    commonStore: CommonStore;
    accountStore: AccountStore;
    userStore: UserStore;
}

export const store: Store = {
    commonStore: new CommonStore(),
    accountStore: new AccountStore(),
    userStore: new UserStore()
};

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}
