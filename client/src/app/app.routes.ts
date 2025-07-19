import { Routes } from '@angular/router';
import { RegisterExam } from './Exams/register-exam/register-exam';
import { RegisterStudent } from './Students/register-student/register-student';
import { RegisterSubject } from './Subjects/register-subject/register-subject';
import { Welcome } from './Welcome/welcome/welcome';

export const routes: Routes = [
    {path:"", component: Welcome, data: {pageTitle: "Xoş Gəlmişsiniz!"}},
    {path:"registersubject", component:RegisterSubject, data: {pageTitle: "Dərslərin Qeydiyyatı"}},
    {path:"registerstudent",component:RegisterStudent, data: {pageTitle: "Şagirdlərin Qeydiyyatı"}},
    {path:"registerexam",component:RegisterExam, data: {pageTitle: "İmtahanların qeydiyyatı"}},
    { path: '', redirectTo: '/welcome', pathMatch: 'full' },
    { path: '**', redirectTo: '/welcome' } // fallback
];
