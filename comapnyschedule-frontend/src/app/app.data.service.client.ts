import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';


export interface Schedule {
  companyId: string;
  schedule: string[];
}

@Injectable()
export class DataServciceClient {
  constructor(private http: HttpClient) { }

  scheduleUrl = 'https://localhost:44398/all'

  getSchedule() {
    return this.http.get<Schedule[]>(this.scheduleUrl)
      .pipe(
        retry(3),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
      console.error('An error occurred:', error.error);

      return throwError(
        'That is nooo gooood');
  }
}
