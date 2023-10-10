import { Injectable } from '@angular/core';
import {FormBuilder, FormGroup, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {


  constructor(private fb: FormBuilder) {}

  createBoxForm(): FormGroup {
    return this.fb.group({
      boxName: ['', this.getBoxNameValidators()],
      price: ['', this.getPriceValidators()],
      boxWidth: ['', this.getBoxWidthValidators()],
      boxLength: ['', this.getBoxLengthValidators()],
      boxHeight: ['', this.getBoxHeightValidators()],
      boxThickness: ['', this.getBoxThicknessValidators()],
      boxColor: ['', this.getBoxColorValidators()],
      boxImgUrl: ['', this.getBoxImgUrlValidators()]
    });
  }


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
