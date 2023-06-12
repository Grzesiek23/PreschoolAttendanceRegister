import { createContext, useContext } from 'react';
import CommonStore from './commonStore';
import AccountStore from "./accountStore";

interface Store {
    commonStore: CommonStore;
    accountStore: AccountStore;
}

export const store: Store = {
    commonStore: new CommonStore(),
    accountStore: new AccountStore()
};

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}
