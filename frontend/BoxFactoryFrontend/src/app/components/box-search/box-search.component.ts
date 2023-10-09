import { Component, OnInit } from '@angular/core';
import {Box, SearchBox } from 'src/app/models/box';
import { BoxService } from 'src/app/services/box.service';

@Component({
  selector: 'app-box-search',
  templateUrl: './box-search.component.html',
  styleUrls: ['./box-search.component.scss'],
})
export class BoxSearchComponent  implements OnInit {
  criteria: SearchBox = {};
  results: Box[] = [];

  constructor(private boxService: BoxService) { }

  ngOnInit() {}

  search() {
    this.boxService.searchBoxes(this.criteria).subscribe(
      data => this.results = data,
      error => console.error('Error fetching search results:', error)
    );
  }
}
