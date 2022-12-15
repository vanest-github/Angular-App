import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OddsViewerComponent } from './odds-viewer/odds-viewer.component';
import { OddsViewerService } from './service/odds-viewer.service';

@NgModule({
  declarations: [
    AppComponent,
    OddsViewerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    OddsViewerService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
