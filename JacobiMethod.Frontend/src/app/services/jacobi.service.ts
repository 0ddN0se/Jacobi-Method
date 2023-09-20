import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {JacobiResponse} from "../dtos/JacobiResponse";
import {JacobiRequest} from "../dtos/JacobiRequest";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class JacobiService {

  constructor(private http: HttpClient) {
  }

  calculateJacobi(jacobiRequest: JacobiRequest): Observable<JacobiResponse> {
    const url = environment.API_URL + 'jacobi';
    return this.http.post<JacobiResponse>(url, jacobiRequest);
  }
}
