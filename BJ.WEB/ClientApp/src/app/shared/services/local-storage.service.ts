import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})

export class LocalStorageService {
  constructor() { }

  public clear() : void {
    localStorage.clear();
  }

  public removeItem(key: string) : void {
    localStorage.removeItem(key);
  }

  public setItem<T>(key: string, data: T): void {
    localStorage.setItem(key, JSON.stringify(data));
  }

  public getItem<T>(key: string) : T {
    try {
      let response: T
     response = <T>JSON.parse(localStorage.getItem(key));
     return <T>response;
    } catch (e) {
      console.log('Error getting data from localStorage', e);
      return null;
    }
  }
}