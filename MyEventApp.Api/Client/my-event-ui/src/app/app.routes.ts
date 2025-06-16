import { Routes } from '@angular/router';
import { EventsComponent } from './components/events/events.component';
import { SalesSummaryComponent } from './components/sales-summary/sales-summary.component';

export const routes: Routes = [{ path: 'sales', component: SalesSummaryComponent },
  { path: 'events', component: EventsComponent },
  { path: '', redirectTo: 'events', pathMatch: 'full' },];
