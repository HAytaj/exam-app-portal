import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterExam } from './register-exam';

describe('RegisterExam', () => {
  let component: RegisterExam;
  let fixture: ComponentFixture<RegisterExam>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterExam]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterExam);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
