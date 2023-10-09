import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { Box, SearchBox } from '../models/box';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BoxService {

  readonly url = 'http://localhost:5000/api'

  constructor(private http : HttpClient) {  }

  getBoxes(): Observable<Box[]> {
    return this.http.get<Box[]>(`${this.url}/boxes`);
  }

  createBox(request: Box): Observable<Box>{
    return this.http.post<Box>(`${this.url}/box`, request);
  }

  getBoxById(id: number): Observable<Box> {
    return this.http.get<Box>(`${this.url}/boxes/${id}`);
  }

  updateBox(id: number, box: Box): Observable<Box>{
    return this.http.put<Box>(`${this.url}/update/${id}`, box);
  }

  searchBoxes(criteria: SearchBox): Observable<Box[]> {
    return this.http.get<Box[]>(`${this.url}/search`, { params: criteria as any });
  }



}
