import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AssignmentAddNewComponent } from '../assignment-add-new/assignment-add-new.component';
import { AssignmentDetailsService } from '../assignment-details/assignment-details.service';
import { IAssignment } from '../model/assignment';
import { IPagination } from '../model/pagination';


@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.css']
})
export class ToDoListComponent implements OnInit {
  pageSize = 3;
  totalAssignment = 10;
  assignments: IAssignment[] = [];
  currentPage = 1;

  constructor(private _assignmnetService: AssignmentDetailsService,  public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getAssignments(1,3);
  }

  getAssignments(page: number, itemsPerPage: number){
    return this._assignmnetService.getAssignmets(page, itemsPerPage)
      .subscribe((response:any) => { 
        this.assignments = response.items;
        this.totalAssignment = response.total;
      });
  }
  
  onPageChanged(pagination: IPagination) {
    this.currentPage = pagination.page;
    this.pageSize = pagination.itemsPerPage;
    this.getAssignments(pagination.page, pagination.itemsPerPage)
  }

  addAssignment(){
    const dialogRef = this.dialog.open(AssignmentAddNewComponent, {
      width: '600px',
      height: '730px',
      data: {
      }
    }); 
    setTimeout(() => {
      dialogRef.close();
    }, 200000);

    dialogRef.afterClosed().subscribe(
      response => {
        this.getAssignments(this.currentPage, this.pageSize)
      }
    )
  }
}
