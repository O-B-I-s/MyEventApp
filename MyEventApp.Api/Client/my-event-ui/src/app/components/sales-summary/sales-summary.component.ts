import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { EventSales } from '../../models';
import { ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSort, MatSortModule } from '@angular/material/sort';

@Component({
  selector: 'app-sales-summary',
  imports: [
    CommonModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
     MatButtonModule,
    MatCardModule,
  ],
  templateUrl: './sales-summary.component.html',
  styleUrl: './sales-summary.component.css',
})
export class SalesSummaryComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['eventId', 'totalQuantity', 'totalRevenue'];
  dataSource = new MatTableDataSource<EventSales>([]);
  error?: string;
  viewBy: 'quantity' | 'revenue' = 'quantity';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.loadData();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  loadData(): void {
    const obs =
      this.viewBy === 'quantity'
        ? this.api.fetchTop5Quantity()
        : this.api.fetchTop5Revenue();

    obs.subscribe({
      next: (data) => {
        this.dataSource.data = data;
        console.log(data);
      },
      error: (e) => (this.error = e.message),
    });
  }

  switchView(view: 'quantity' | 'revenue'): void {
    this.viewBy = view;
    this.loadData();
  }
  applyFilter(evt: Event) {
    const filter = (evt.target as HTMLInputElement).value.trim().toLowerCase();
    this.dataSource.filter = filter;
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
