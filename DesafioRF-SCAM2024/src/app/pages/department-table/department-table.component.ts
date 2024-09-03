import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-department-table',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './department-table.component.html',
  styleUrl: './department-table.component.scss',
})
export class DepartmentTableComponent implements OnInit {
  departments: any[] = [];
  departmentForm: FormGroup;
  editingDepartmentId: number | null = null;

  constructor(
    private apiService: ApiService,
    private fb: FormBuilder,
    private toastService: ToastrService
  ) {
    this.departmentForm = this.fb.group({
      name: ['', Validators.required],
    });
  }

  ngOnInit(): void {
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

  editDepartment(department: any): void {
    this.editingDepartmentId = department.id;
    this.departmentForm.setValue({ name: department.name });
  }

  createDepartment(): void {
    if (this.departmentForm.valid) {
      this.apiService.createDepartment(this.departmentForm.value).subscribe({
        next: () => this.toastService.success('Cadastro feito com sucesso!'),
        error: () =>
          this.toastService.error(
            'Erro inesperado! Tente novamente mais tarde'
          ),
      });
    }
  }

  updateDepartment(): void {
    if (this.departmentForm.valid && this.editingDepartmentId !== null) {
      const name = this.departmentForm.get('name')?.value;

      this.apiService
        .updateDepartment(this.editingDepartmentId, name)
        .subscribe({
          next: () => {
            this.toastService.success('Edição feito com sucesso!');
            this.loadDepartments();
            this.departmentForm.reset();
            this.editingDepartmentId = null;
          },
          error: () =>
            this.toastService.error(
              'Erro inesperado! Tente novamente mais tarde'
            ),
        });
    }
  }

  deleteDepartment(id: number): void {
    this.apiService.deleteDepartment(id).subscribe({
      next: () => {
        this.toastService.success('Deletado com sucesso!');
        this.loadDepartments();
      },
      error: (error) => {
        const errorMessage =
          error.error.message || 'Erro ao carregar departamentos.';
        this.toastService.error(errorMessage);
      },
    });
  }
}
