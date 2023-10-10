import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import { Box } from 'src/app/models/box';
import { BoxService } from 'src/app/services/box.service';

@Component({
  selector: 'app-box',
  templateUrl: './box.component.html',
  styleUrls: ['./box.component.scss'],
})
export class BoxComponent  implements OnInit {

  box: Box | null = null;



  constructor(private route: ActivatedRoute,
              private service: BoxService,
              private router: Router) { }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id !== 0) {
      this.service.getBoxById(id).subscribe(result => {
        this.box = result;
      });
    }
  }
  deleteBox(id: number) {
    if (confirm('Are you sure you want to delete this box?')) {
      const id = Number(this.route.snapshot.paramMap.get('id'));
      this.service.deleteBox(id).subscribe(() => {
          alert('Box deleted successfully!');
          // Navigate to the box-list page after successful deletion
          this.router.navigate(['/boxes']);
        },
        error => {
          console.error('Error deleting box:', error);
        }
      );
    }
  }

  goToUpdatePage(boxId: number) {
    this.router.navigate(['/update', boxId]);
  }


}
