import { Component, OnInit } from '@angular/core';
import { BoxService } from 'src/app/services/box.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidationService } from 'src/app/services/validation.service';
import { Box } from 'src/app/models/box';

@Component({
  selector: 'app-box-create',
  templateUrl: './box-create.component.html',
  styleUrls: ['./box-create.component.scss'],
})
export class BoxCreateComponent implements OnInit {
  fg: FormGroup;

  constructor(private service: BoxService, private fb: FormBuilder, private validation: ValidationService) {
    this.fg = this.fb.group({
      boxName: ['', this.validation.getBoxNameValidators()],
      price: ['', this.validation.getPriceValidators()],
      boxWidth: ['', this.validation.getBoxWidthValidators()],
      boxLength: ['', this.validation.getBoxLengthValidators()],
      boxHeight: ['', this.validation.getBoxHeightValidators()],
      boxThickness: ['', this.validation.getBoxThicknessValidators()],
      boxColor: ['', this.validation.getBoxColorValidators()],
      boxImgUrl: ['', this.validation.getBoxImgUrlValidators()]
    });
  }


  public submitCreating(): void {
    if(this.fg.valid){
      this.service.createBox(this.fg.value).subscribe(
        (response: Box) => {
          console.log("Response from server:", response)
        }
      )
    }
  }

  ngOnInit() {}
}
