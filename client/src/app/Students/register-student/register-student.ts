import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register-student',
  imports: [FormsModule, CommonModule, MatSnackBarModule],
  templateUrl: './register-student.html',
  styleUrl: './register-student.css'
})
export class RegisterStudent implements OnInit {

  classes: any[] = [];
  students: Student[] = [];
  selectedClassId: string = '';

  studentData: Student = this.getEmptyStudent();

  readonly API = {
    base: 'https://localhost:7080/api',
    get students() { return `${this.base}/Students`; },
    get studentsFull() { return `${this.base}/Students/with-class`; },
    get teachers() { return `${this.base}/Teachers`; },
    get classes() { return `${this.base}/Classes`; }
  };

  private readonly httpOptions = {
    headers: new HttpHeaders({
      Authorization: 'my-auth-token',
      'Content-Type': 'application/json',
    }),
  };

  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }  // constructor

  ngOnInit(): void {
    this.loadClasses();
    this.loadStudents();
  }

  loadClasses(): void {
    this.http.get<any[]>(this.API.classes).subscribe({
      next: (data) => (this.classes = data),
      error: (err) => console.error('API xətası (Classes):', err),
    });
  }

  loadStudents(): void {
    this.http.get<Student[]>(this.API.studentsFull).subscribe({
      next: (data) => (this.students = data),
      error: (err) => console.error('API xətası (Students):', err),
    });
  }

  onClassChange(): void {
    this.studentData.classId = this.selectedClassId;
  }

  isDuplicateStudentNumber(student: Student): boolean {
      return this.students.some((s) => s.studentNumber === student.studentNumber && s.id != this.studentData.id); 
  }

  resetStudentData(): void {
    this.studentData = this.getEmptyStudent();
    this.selectedClassId = '';
  }

  private showError(message: string) {
    this.snackBar.open(message, 'Bağla', {
      duration: 3000,
      panelClass: ['snackbar-error'],
    });
  }

  private showSuccess(message: string) {
    this.snackBar.open(message, 'Bağla', {
      duration: 3000,
      panelClass: ['snackbar-success'],
    });
  }

  afterFormPost(form: NgForm): void {
    form.resetForm();
    this.loadStudents();
    this.resetStudentData();
  }

  onUpdateStudent(student: Student): void {
    this.studentData = { ...student };
    this.selectedClassId = student.classId;

    this.showSuccess('Şagird məlumatlarını yeniləyə bilərsiniz.');
  }

  onDeleteStudent(student: Student): void {
    const url = `${this.API.students}/${student.id}`;
    this.http.delete(url).subscribe({
      next: () => {
        this.showSuccess('Şagird uğurla silindi!');
        this.loadStudents();
      },
      error: (err) => {
        this.showError('Şagirdi silərkən xıta baş verdi!');
        console.error('API xətası:', err);
      },
    });
  }

  onSubmit(form: NgForm): void {
    if (form.invalid) {
      this.showError('Formu tam doldurun!');
      return;
    }

    if (this.isDuplicateStudentNumber(this.studentData)) {
      this.showError('Bu tələbə nömrəsi artıq mövcuddur!');
      return;
    }

    const isNew = this.studentData.id === 0;
    const url = isNew
      ? this.API.students
      : `${this.API.students}/${this.studentData.id}`;
    const method = isNew ? 'post' : 'put';

    this.http[method](url, this.studentData, this.httpOptions).subscribe({
      next: (res) => console.log('Server cavabı:', res),
      error: (err) => {
        console.error('Xəta baş verdi:', err);
        this.showError('Xəta baş verdi!');
      },
      complete: () => {
        this.afterFormPost(form);
        this.showSuccess(
          isNew ? 'Şagird uğurla qeydə alındı!' : 'Şagird uğurla yeniləndi!'
        );
      },
    });
  }

  private getEmptyStudent(): Student {
    return {
      id: 0,
      studentNumber: null,
      studentName: '',
      studentSurname: '',
      classId: '',
      classNumber:''
    };
  }


}
