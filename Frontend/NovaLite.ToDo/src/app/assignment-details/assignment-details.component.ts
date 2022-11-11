import { Component, OnInit } from '@angular/core';
import { IAssignment } from '../model/assignment';
import { AssignmentDetailsService } from './assignment-details.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IStep } from '../model/step';
import { StepService } from '../step-details/step.service';
import { IPagination } from '../model/pagination';
import { MatDialog } from '@angular/material/dialog';
import { StepDetailsComponent } from '../step-details/step-details.component';
import { AttachmentService } from './attachment.service';

@Component({
  selector: 'app-assignment-details',
  templateUrl: './assignment-details.component.html',
  styleUrls: ['./assignment-details.component.css']
})
export class AssignmentDetailsComponent implements OnInit {
  pageSize = 3;
  totalSteps = 0;
  steps: IStep[] = [];
  assignment: IAssignment = {number: 0, subject: "", description: "",  reminder: new Date() , isExpired: false, attachment: { id: "", name: ""}};
  assignmentNumber!: number;
  pageNumber = 1;
  step: IStep = { number: -1, subject: "", completed: false};
  file: any;
  currentFile!: File;
  change: boolean = false;

  constructor(private _assignmentDetailsService: AssignmentDetailsService, private route: ActivatedRoute,
    private router: Router, private _stepService : StepService, public dialog: MatDialog, private _attachmentService: AttachmentService) { }

  ngOnInit(): void {
    this.assignmentNumber = Number(this.route.snapshot.paramMap.get('id'));
    this.getAssignment();
    this.getSteps(1, this.pageSize);
  }

  getAssignment(){
    return this._assignmentDetailsService.getAssignment(this.assignmentNumber).subscribe(
      response => {
        this.assignment = response.result;
      }
    )
  }

  deleteAssignment(){
    if(confirm("Are you sure to delete this assignment?")){
      return this._assignmentDetailsService.deleteAssignment(this.assignmentNumber).subscribe(
      response =>{
        this.router.navigate(['/']);
      }
    )
    } else {
      return;
    }
  }

  onFileChange(event : any) {
    const maxAllowedSize = 4 * 1024 * 1024;
    if (event.target.files[0].size > maxAllowedSize){
      alert("You're file is too big, please choose another file!")
    }
    this.currentFile = event.target.files[0];
  }

  changeAssignment() {
    if (!this.currentFile){
      this._assignmentDetailsService.changeAssignment(this.assignment, "").subscribe(
        response => {
          this.assignment = response;
          this.router.navigate(['/']);
        })
    } else {
      this.assignment = this._attachmentService.uploadFile(this.getFilename(this.file), this.currentFile, this.assignment);
      this.router.navigate(['/']);
    }
  }

  getFilename(path: string) : string{
      return path.split('\\')[2].split('.')[0];
  }

  cancel(){
    this.router.navigate(['/']);
  }

  getSteps(page: number, pageSize: number){
    return this._stepService.getSteps(page, pageSize, this.assignmentNumber)
      .subscribe((response:any)=> { 
        this.steps = response.items;
        this.totalSteps = response.total;
      });
  }

  onPageChanged(pagaination: IPagination) {
    this.pageSize = pagaination.itemsPerPage;
    this.pageNumber = pagaination.page;
    this.getSteps(pagaination.page, pagaination.itemsPerPage)
  }

  showDialog(step: IStep, update: boolean){
    const dialogRef = this.dialog.open(StepDetailsComponent, {
      width: '500px',
      height: '400px',
      data: {
        update: update,
        number: step['number'],
        subject: step['subject'],
        completed: step['completed'],
        assignmentNumber: this.assignmentNumber
      }
    }); 
    setTimeout(() => {
      dialogRef.close();
    }, 20000);

    dialogRef.afterClosed().subscribe(
      response => {
        if (response.data == "updated"){
          this.getSteps(this.pageNumber, this.pageSize);
        }
      }
    )
  }

  changeAttachment(){
    this._attachmentService.deleteAttachment(this.assignment.attachment.id).subscribe(
      response => {
        this.change = true;
      }
    );
  }

  downloadAttachment(fileName: string){
    this._attachmentService.downloadFile(this.assignment.attachment.id, fileName);
  }
}
