import { Component, OnInit } from '@angular/core';
import { BoxService } from 'src/app/services/box.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-box-create',
  templateUrl: './box-create.component.html',
  styleUrls: ['./box-create.component.scss'],
})
export class BoxCreateComponent implements OnInit {
  fg: FormGroup;

  constructor(private service: BoxService, private fb: FormBuilder) {
    this.fg = this.fb.group({
      boxName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(12)]],
      price: ['', Validators.required],
      boxWidth: [''],
      boxLength: [''],
      boxHeight: [''],
      boxThickness: [''],
      boxColor: [''],
      boxImgUrl: ['']
    });
  }

  get boxName(): FormControl { return this.fg.get('boxName') as FormControl; }
  get price(): FormControl { return this.fg.get('price') as FormControl; }
  get boxWidth(): FormControl { return this.fg.get('boxWidth') as FormControl; }
  get boxLength(): FormControl { return this.fg.get('boxLength') as FormControl; }
  get boxHeight(): FormControl { return this.fg.get('boxHeight') as FormControl; }
  get boxThickness(): FormControl { return this.fg.get('boxThickness') as FormControl; }

  public submitCreating(): void {
    this.service.createBox({
      boxName: this.fg.get('boxName')?.value,
      price: this.fg.get('price')?.value,
      boxWidth: this.fg.get('boxWidth')?.value,
      boxLength: this.fg.get('boxLength')?.value,
      boxHeight: this.fg.get('boxHeight')?.value,
      boxThickness: this.fg.get('boxThickness')?.value,
      boxColor: this.fg.get('boxColor')?.value,
      boxImgUrl: this.fg.get('boxImgUrl')?.value,
    }).subscribe(() => {
      window.location.reload();
    });
  }

  ngOnInit() {}
}
