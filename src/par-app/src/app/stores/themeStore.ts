import { makeAutoObservable } from 'mobx';

export default class ThemeStore {
    constructor() {
        makeAutoObservable(this);
    }
}
