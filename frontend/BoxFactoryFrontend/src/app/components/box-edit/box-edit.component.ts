import { Component, OnInit } from '@angular/core';
import { BoxService } from 'src/app/services/box.service';
import {FormGroup} from "@angular/forms";
import { ValidationService } from 'src/app/services/validation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Box } from 'src/app/models/box';

@Component({
  selector: 'app-box-edit',
  templateUrl: './box-edit.component.html',
  styleUrls: ['./box-edit.component.scss'],
})
export class BoxEditComponent  implements OnInit {

  public fg: FormGroup;
  box!: Box;

  constructor(private service: BoxService,
              private validation: ValidationService,
              private route: ActivatedRoute,
              private router: Router) {
    this.fg = this.validation.createBoxForm();
  }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    // Fetch the box details first
    this.service.getBoxById(id).subscribe(box => {
      this.box = box;
      this.fg.patchValue(box);  // Update form controls with retrieved box data
    });
  }


  updateBox() {
    if (this.box?.id && this.fg.valid) {
      const updateBoxData = this.fg.value;
      this.service.updateBox(this.box.id, updateBoxData).subscribe(updatedBox => {
        this.box = updatedBox;
        alert('Box updated successfully!');
        this.router.navigate(['/boxes']);
      }, error => {
        console.error('Error updating box:', error);
      });
    } else {
      alert('Please fill out the form correctly before updating.');
    }
  }

}
