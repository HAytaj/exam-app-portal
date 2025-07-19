import { Component, signal } from '@angular/core';
import { RouterOutlet,RouterModule  } from '@angular/router';
import { RegisterExam } from './Exams/register-exam/register-exam';
import { RegisterStudent } from './Students/register-student/register-student';
import { RegisterSubject } from './Subjects/register-subject/register-subject';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { filter, map, mergeMap } from 'rxjs/operators';
import { CommonModule } from '@angular/common';
import { Welcome } from './Welcome/welcome/welcome';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterModule,RegisterExam, RegisterSubject, RegisterStudent, CommonModule, Welcome,
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ExamApp');
  pageTitle = '';
  welcome = true;
  constructor(private router: Router, private route: ActivatedRoute) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      map(() => {
        let child = this.route.firstChild;
        while (child?.firstChild) {
          child = child.firstChild;
        }
        return child;
      }),
      mergeMap(route => route?.data ?? [])
    ).subscribe(data => {
      this.pageTitle = data['pageTitle'] || 'Xoş Gəlmişsiniz';
    });
  }
}
