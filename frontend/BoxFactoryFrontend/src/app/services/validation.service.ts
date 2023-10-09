import { Injectable } from '@angular/core';
import { Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {

  getBoxNameValidators() {
    return [Validators.required, Validators.minLength(4), Validators.maxLength(12)];
  }

  getPriceValidators() {
    return [Validators.required, Validators.min(5), Validators.max(27)];
  }

  getBoxWidthValidators() {
    return [Validators.required, Validators.min(15), Validators.max(125)];
  }

  getBoxLengthValidators() {
    return [Validators.required, Validators.min(15), Validators.max(220)];
  }

  getBoxHeightValidators() {
    return [Validators.required, Validators.min(15), Validators.max(100)];
  }

  getBoxThicknessValidators() {
    return [Validators.required, Validators.min(1), Validators.max(5)];
  }

  getBoxColorValidators() {
    return [];
  }

  getBoxImgUrlValidators() {
    return [];
  }
}
