import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Box } from 'src/app/models/box';
import { BoxService } from 'src/app/services/box.service';

@Component({
  selector: 'app-box-list',
  templateUrl: './box-list.component.html',
  styleUrls: ['./box-list.component.scss'],
})
export class BoxListComponent  implements OnInit {

  boxes: Box[]= [];
  constructor( private service: BoxService, private router: Router) { }

  ngOnInit() {
     this.service.getBoxes().subscribe(result => {
      this.boxes = result
    })
  }

  showBoxDetails(boxId: number) {
    this.router.navigate(['/box', boxId]);
  }

}
