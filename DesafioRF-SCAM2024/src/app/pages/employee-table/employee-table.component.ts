import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
  FormsModule,
} from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee-table',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './employee-table.component.html',
  styleUrls: ['./employee-table.component.scss'],
})
export class EmployeeTableComponent implements OnInit {
  employees: any[] = [];
  employeeForm: FormGroup;
  editingEmployeeId: number | null = null;
  departments: any[] = [];

  constructor(
    private apiService: ApiService,
    private fb: FormBuilder,
    private toastService: ToastrService
  ) {
    this.employeeForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      position: ['', Validators.required],
      departmentId: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.loadEmployees();
    this.loadDepartments();
  }

  loadDepartments(): void {
    this.apiService.getDepartments().subscribe({
      next: (data: any[]) => {
        this.departments = data;
      },
      error: (error) => {
        this.toastService.error(
          'Erro ao carregar departamentos. Tente novamente mais tarde.'
        );
      },
    });
  }

  loadEmployees(): void {
    this.apiService.getEmployees().subscribe({
      next: (data: any[]) => {
        this.employees = data;
      },
      error: (error) => {
        this.toastService.error(
          'Erro ao carregar Empregados. Tente novamente mais tarde.'
        );
      },
    });
  }

  editEmployee(employee: any): void {
    this.editingEmployeeId = employee.id;
    this.employeeForm.setValue({
      name: employee.name,
      email: employee.email,
      position: employee.position,
      departmentId: employee.department.id,
    });
  }

  createEmployee(): void {
    if (this.employeeForm.valid) {
      this.apiService.createEmployee(this.employeeForm.value).subscribe({
        next: () => {
          this.toastService.success('Empregado cadastrado com sucesso!');
          this.loadEmployees();
          this.employeeForm.reset();
        },
        error: () =>
          this.toastService.error(
            'Erro inesperado! Tente novamente mais tarde'
          ),
      });
    }
  }

  updateEmployee(): void {
    if (this.employeeForm.valid && this.editingEmployeeId !== null) {
      this.apiService
        .updateEmployee(this.editingEmployeeId, this.employeeForm.value)
        .subscribe({
          next: () => {
            this.toastService.success('Edição feito com sucesso!');
            this.loadEmployees();
            this.employeeForm.reset();
            this.editingEmployeeId = null;
          },
          error: () =>
            this.toastService.error(
              'Erro inesperado! Tente novamente mais tarde'
            ),
        });
    }
  }

  deleteEmployee(id: number): void {
    if (confirm('Tem certeza que deseja excluir este Empregado?')) {
      this.apiService.deleteEmployee(id).subscribe({
        next: () => {
          this.toastService.success('Empregado Deletado com sucesso!');
          this.loadEmployees();
        },
        error: () =>
          this.toastService.error(
            'Erro inesperado! Tente novamente mais tarde'
          ),
      });
    }
  }
}
