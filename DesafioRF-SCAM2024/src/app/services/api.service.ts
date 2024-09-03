import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private apiUrl = 'http://localhost:5014/api/';

  constructor(private http: HttpClient) {}

  getDepartments(): Observable<any> {
    return this.http.get<any>(this.apiUrl + 'departments');
  }

  createDepartment(department: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'departments', department);
  }

  updateDepartment(id: number, name: string): Observable<any> {
    const url = `${this.apiUrl + 'departments'}/${id}`;
    return this.http.put(url, { name, id });
  }

  deleteDepartment(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl + 'departments'}/${id}`);
  }

  getEmployees(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + 'employees');
  }

  createEmployee(employee: any): Observable<any> {
    return this.http.post(this.apiUrl + 'employees', employee);
  }

  updateEmployee(id: number, employee: any): Observable<any> {
    const url = `${this.apiUrl + 'employees'}/${id}`;
    return this.http.put(url, employee);
  }

  deleteEmployee(id: number): Observable<any> {
    const url = `${this.apiUrl + 'employees'}/${id}`;
    return this.http.delete(url);
  }
}
