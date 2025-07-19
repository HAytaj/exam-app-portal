import { Directive } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

@Directive({
  selector: '[appEnglishLettersOnly]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: EnglishLettersOnly,
      multi: true
    }
  ]
})
export class EnglishLettersOnly implements Validator  {

  validate(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    const pattern = /^[A-Z]+$/;

    if (value && !pattern.test(value)) {
      console.log("isleyirem " + value);
      return { englishLettersOnly: true };
    }
    return null;
  }
  

}
