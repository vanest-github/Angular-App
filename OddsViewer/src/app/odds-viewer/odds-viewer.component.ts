import { Component, OnInit } from '@angular/core';
import { OddsViewerService } from '../service/odds-viewer.service';
import { OddsApiResponse } from '../model/odds-api-response.model';

@Component({
  selector: 'app-odds-viewer',
  templateUrl: './odds-viewer.component.html',
  styleUrls: ['./odds-viewer.component.css']
})
export class OddsViewerComponent {
  public title = 'Odds Viewer';
  public totalCount: number;
  public drawnCount: number;
  public oddsApiResponse: OddsApiResponse;

  constructor(private oddsViewerService: OddsViewerService) {
  }

  getOdds(total: number, drawn: number) {
    this.oddsViewerService.getOdds(total, drawn).subscribe(response => {
      this.oddsApiResponse = response as OddsApiResponse;
    }, error => {
      this.oddsApiResponse = null;
      console.log(error);
    });
  }
}
