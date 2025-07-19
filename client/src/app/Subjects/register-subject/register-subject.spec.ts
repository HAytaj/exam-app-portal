import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterSubject } from './register-subject';

describe('RegisterSubject', () => {
  let component: RegisterSubject;
  let fixture: ComponentFixture<RegisterSubject>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterSubject]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterSubject);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
