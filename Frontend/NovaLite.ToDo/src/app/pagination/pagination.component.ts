import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IPagination } from '../model/pagination';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {
  @Input() itemsPerPage = 3;
  @Input() totalItems = 0;
  
  currentPage = 1;

  @Output() pageChangedEvent = new EventEmitter<IPagination>()
  
  constructor() { }

  ngOnInit(): void { }

  goTo(pageNumber: number){
   this.currentPage = pageNumber;
   this.pageChangedEvent.emit({"page": this.currentPage, "itemsPerPage":this.itemsPerPage});
  }

  refresh(){
    this.pageChangedEvent.emit({"page": this.currentPage, "itemsPerPage":this.itemsPerPage});
  }

  getPages() {
    const pages: number[] = [];
    const numberOfPages = this.totalItems / this.itemsPerPage;

    if (this.totalItems <= 0 || this.totalItems == this.itemsPerPage){
      return [1];
    }

    for (let i = 0; i < numberOfPages; i++) {
      pages.push(i + 1);
    }

    return pages;
  }

}
