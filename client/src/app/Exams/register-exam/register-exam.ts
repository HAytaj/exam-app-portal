import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { EnglishLettersOnly } from '../../validators/english-letters-only';
import { ErrorService } from '../../services/error';

@Component({
  selector: 'app-register-exam',
  imports: [FormsModule, CommonModule, MatSnackBarModule, EnglishLettersOnly],
  templateUrl: './register-exam.html',
  styleUrl: './register-exam.css'
})


export class RegisterExam implements OnInit {
  exams: Exam[] = [];
  subjects: Subject[] = [];
  students: Student[] = [];

  constructor(private httpClient: HttpClient, private snackBar: MatSnackBar, private errorService: ErrorService) { }  // constructor


  examData: Exam = this.createEmptyExam();

   readonly API = {
    base: 'https://localhost:7080/api',
    get subjects() { return `${this.base}/Subjects`; },
    get examsFull() { return `${this.base}/Exams/with-subjects-students`; },
    get exams() { return `${this.base}/Exams`; },
    get students() { return `${this.base}/Students`; }
  };

   httpOptions = {
    headers: new HttpHeaders({
      Authorization: "my-auth-token",
      'Content-Type': 'application/json'
    })
  };

   createEmptyExam(): Exam {
    return {
      id: 0,
      code: '',
      studentNumber: null,
      examDate: '',
      score: null
    };
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

 resetExamData(): void {
    this.examData = this.createEmptyExam();
  }
  
  ngOnInit(): void {
    this.loadExams();
    this.loadStudents();
    this.loadSubjects();
  }


  resetForm(form: NgForm): void {
    this.examData = this.createEmptyExam();
    form.resetForm();
    this.loadExams();
    this.resetExamData();
  }

  onUpdateExam(exam: any): void {
    this.examData = { ...exam };
    this.showSuccess('İmtahan məlumatlarını yeniləyə bilərsiniz.');
  }

  onDeleteExam(exam: any): void {
    const deleteApiUrl = `${this.API.exams}/${exam.id}`;
    this.httpClient.delete(deleteApiUrl).subscribe({
      next: () => {
        this.showSuccess('İmtahan uğurla silindi!');
        this.loadExams();
      },
      error: (err) => {
        console.error('API xətası:', err);
      }
    });
  }

   loadExams(): void {
    const apiUrl = this.API.examsFull;
    this.httpClient.get<Exam[]>(apiUrl).subscribe({
      next: data => this.exams = data,
      error: err => console.error('API xətası:', err)
    });
  }


  loadStudents(): void {
    const apiUrl = this.API.students;
    this.httpClient.get<Student[]>(apiUrl).subscribe({
      next: data => this.students = data,
      error: err => console.error('API xətası:', err)
    });
  }

  loadSubjects(): void {
    const apiUrl = this.API.subjects;
    this.httpClient.get<Subject[]>(apiUrl).subscribe({
      next: data => this.subjects = data,
      error: err => console.error('API xətası:', err)
    });
  }

subjectExists(code: string): boolean {
    return this.subjects.some(s => s.code === code);
  }

  studentExists(studentNumber: any): boolean {
    return this.students.some(s => s.studentNumber === studentNumber);
  }

  

  onSubmit(form: NgForm): void {
    if (form.invalid) {
      this.showError("Formu tam doldurun!");
      return;
    }

    const subject = this.subjects.find(s => s.code === this.examData.code);
    const student = this.students.find(s => s.studentNumber === this.examData.studentNumber);

    if (!subject) {
      this.showError("Bu koda uyğun fənn tapılmadı.");
      return;
    }

    if (!student) {
      this.showError("Bu nömrəyə uyğun şagird tapılmadı.");
      return;
    }

    const payload = {
      ...this.examData,
      subjectId: subject.id,
      studentId: student.id
    };

    const url = this.examData.id === 0
      ? this.API.exams
      : `${this.API.exams}/${this.examData.id}`;

    const request = this.examData.id === 0
      ? this.httpClient.post(url, payload, this.httpOptions)
      : this.httpClient.put(url, payload, this.httpOptions);

    request.subscribe({
      next: res => console.log("Cavab:", res),
      error: err => {
        console.error(err);
        this.errorService.handle(err);
      },
      complete: () => {

        this.examData.id === 0 ? this.showSuccess('İmtahan uğurla saxlanıldı!') : this.showSuccess('İmtahan uğurla yeniləndi!')
        this.resetForm(form);
      }
    });
  }
}
