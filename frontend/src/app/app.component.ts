import { Component } from '@angular/core';

import { RegisterComponent } from "../app/components/register/register.component";

@Component({
  selector: 'app-root',
  imports: [RegisterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'frontend';
}
