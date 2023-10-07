import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { Box } from '../models/box';
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

}
