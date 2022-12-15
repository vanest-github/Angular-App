import { Injectable } from "@angular/core";
import { Observable, of } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { IOddsApiResponse } from "../model/iodds-api-response.model"

@Injectable()
export class OddsViewerService {
  constructor(private httpClient: HttpClient) { }

  getOdds(total: number, drawn: number): Observable<IOddsApiResponse> {
    return this.httpClient.get<IOddsApiResponse>('https://localhost:44317/api/v1/odds/' + total + '/' + drawn);
  }
}