import { Component, OnInit } from '@angular/core';
import { BoxService } from 'src/app/services/box.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidationService } from 'src/app/services/validation.service';
import { Box } from 'src/app/models/box';
import { Router } from '@angular/router';

@Component({
  selector: 'app-box-create',
  templateUrl: './box-create.component.html',
  styleUrls: ['./box-create.component.scss'],
})
export class BoxCreateComponent implements OnInit {

  public fg: FormGroup;

  constructor(private service: BoxService,
              private validation: ValidationService,
              private router: Router) {
    this.fg = this.validation.createBoxForm();
  }



  public submitCreating(): void {
    if(this.fg.valid){
      this.service.createBox(this.fg.value).subscribe(
        (response: Box) => {
          console.log("Response from server:", response);
          // Navigate to the box-list page after successful deletion
          this.router.navigate(['/boxes']);
        }
      )
    }
  }

  ngOnInit() {}
}
