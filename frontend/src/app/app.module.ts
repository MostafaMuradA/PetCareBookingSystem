import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component'; // This is a standalone component
import { AuthInterceptor } from './interceptors/auth.interceptor';

@NgModule({
  declarations: [
    // ✅ REMOVE AppComponent from here
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppComponent // ✅ IMPORT it instead of declaring
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [] // ✅ Bootstrap the standalone component
})
export class AppModule {}
