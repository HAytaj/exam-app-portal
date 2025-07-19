import { EnglishLettersOnly } from './english-letters-only';

describe('EnglishLettersOnly', () => {
  it('should create an instance', () => {
    const directive = new EnglishLettersOnly();
    expect(directive).toBeTruthy();
  });
});
