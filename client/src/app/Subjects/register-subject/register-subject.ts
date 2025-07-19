import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ErrorService } from '../../services/error';


@Component({
  selector: 'app-register-subject',
  imports: [FormsModule, CommonModule, MatSnackBarModule],
  templateUrl: './register-subject.html',
  styleUrl: './register-subject.css'
})


export class RegisterSubject implements OnInit {

  subjectData: Subject = this.getEmptySubject();
  subjectDto: SubjectDto = { id: 0, code: '', subjectName: '', classNumber: '', teacherFullName: '' };


  constructor(private http: HttpClient, private snackBar: MatSnackBar, private errorService: ErrorService) { }  // constructor

  subjects: SubjectDto[] = [];
  teachers: any[] = [];
  classes: any[] = [];
  selectedTeacherId = '';
  selectedClassId = '';


  readonly API = {
    base: 'https://localhost:7080/api',
    get subjects() { return `${this.base}/Subjects`; },
    get subjectsFull() { return `${this.base}/Subjects/with-teachers-classes`; },
    get teachers() { return `${this.base}/Teachers`; },
    get classes() { return `${this.base}/Classes`; }
  };


  httpOptions = {
    headers: new HttpHeaders({
      Authorization: 'my-auth-token',
      'Content-Type': 'application/json'
    })
  };

 ngOnInit(): void {
    this.loadSubjects();
    this.loadTeachers();
    this.loadClasses();
  }

 private loadTeachers() {
    this.http.get<any[]>(this.API.teachers).subscribe({
      next: (res) => (this.teachers = res),
      error: (err) => {console.log(err); this.showError('Müəllimləri yükləyərkən xəta baş verdi.')},
    });
  }

  private loadClasses() {
    this.http.get<any[]>(this.API.classes).subscribe({
      next: (res) => (this.classes = res),
      error: (err) => {console.log(err); this.showError('Sinifləri yükləyərkən xəta baş verdi.')},
    });
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

  

  loadSubjects() {
    this.http.get<SubjectDto[]>(this.API.subjectsFull).subscribe({
      next: data => this.subjects = data,
      error: err => console.error('Dərsləri yükləyərkən xəta baş verdi.', err)
    });
  }

  isDuplicateSubjectCode(subject: Subject): boolean {
    return this.subjects.some((s) => s.code.toLowerCase() === subject.code.toLowerCase() && s.id != this.subjectData.id);
  }

   private getEmptySubject() {
    return {
      id: 0,
      code: '',
      subjectName: '',
      teacherId: '',
      classId: '',
    };
  }

   resetForm(form: NgForm): void {
    form.resetForm();
    this.subjectData = this.getEmptySubject();
    this.selectedClassId = '';
    this.selectedTeacherId = '';
    this.loadSubjects();
  }

 
    onSubmit(form: NgForm): void {
    if (form.invalid) {
      this.showError("Formu düzgün doldurun!");
      return;
    }

    const isDuplicate = this.isDuplicateSubjectCode(this.subjectData);
    if (isDuplicate) {
      this.showError('Bu dərs kodu artıq mövcuddur!');
      return;
    }

    const isNew = this.subjectData.id === 0;
    const url = isNew
      ? this.API.subjects
      : `${this.API.subjects}/${this.subjectData.id}`;
    const request = isNew
      ? this.http.post(url, this.subjectData, this.httpOptions)
      : this.http.put(url, this.subjectData, this.httpOptions);

    request.subscribe({
      next: () => {
        this.afterFormSubmit(form, isNew ? 'Dərs uğurla əlavə olundu!' : 'Dərs uğurla yeniləndi!');
      },
      error: () => this.showError('Xəta baş verdi!'),
    });
  }

    afterFormSubmit(form: NgForm, message: string): void {
    form.resetForm();
    this.subjectData = this.getEmptySubject();
    this.selectedClassId = '';
    this.selectedTeacherId = '';
    this.loadSubjects();
    this.showSuccess(message);
  }

  onUpdateSubject(subject: any): void {
    this.subjectData = { ...subject };
    this.selectedClassId = subject.classId;
    this.selectedTeacherId = subject.teacherId;
    this.showSuccess('Dərs məlumatlarını yeniləyə bilərsiniz.');
  }

  onDeleteSubject(subject: any): void {
    const url = `${this.API.subjects}/${subject.id}`;
    this.http.delete(url).subscribe({
      next: () => {
        this.loadSubjects();
        this.showSuccess('Dərs uğurla silindi!');
      },
      error: (err) => this.errorService.handle(err)
    });
  }

  onTeacherChange(): void {
    this.subjectData.teacherId = this.selectedTeacherId;
  }

  onClassChange(): void {
    this.subjectData.classId = this.selectedClassId;
  }
}
