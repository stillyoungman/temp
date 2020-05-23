import { tokenStorageKey } from "../constants";

export function isAuthenticated(){
    // return true;
    return localStorage.getItem(tokenStorageKey);
}