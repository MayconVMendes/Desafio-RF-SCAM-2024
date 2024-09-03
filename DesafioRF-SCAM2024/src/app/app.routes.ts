import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { DepartmentTableComponent } from './pages/department-table/department-table.component';
import { EmployeeTableComponent } from './pages/employee-table/employee-table.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'department-table',
    component: DepartmentTableComponent,
  },
  {
    path: 'employee-table',
    component: EmployeeTableComponent,
  },
];
