import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AssignmentDetailsService } from '../assignment-details/assignment-details.service';
import { AttachmentService } from '../assignment-details/attachment.service';
import { IAssignment } from '../model/assignment';
import { IStep } from '../model/step';

@Component({
  selector: 'app-assignment-add-new',
  templateUrl: './assignment-add-new.component.html',
  styleUrls: ['./assignment-add-new.component.css']
})
export class AssignmentAddNewComponent implements OnInit {
  assignment: IAssignment = {number: 0, subject: "", description: "",  reminder: new Date() , isExpired: false, attachment: { id: "", name: ""}};
  step: IStep = {number: 0, subject: "", completed: false};
  steps!: any[];
  file: any;
  currentFile!: File;
  change: boolean = false;
  addedAssignment!: any;

  constructor(private _assignmentService: AssignmentDetailsService, private router: Router, public dialogRef: MatDialogRef<AssignmentAddNewComponent>, 
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any, private _attachmentService: AttachmentService) { }

  ngOnInit(): void {
    this.steps = [];
  }

  addAssignment(){
    this._assignmentService.addAssignment(this.assignment, this.steps).subscribe(
      response => {
        this.addedAssignment = response.result
        if (this.currentFile){
          this._attachmentService.uploadFile(this.getFilename(this.file), this.currentFile, this.addedAssignment)
        }
        this.dialogRef.close();
      }
    )
  }

  getFilename(path: string) : string{
    return path.split('\\')[2].split('.')[0];
  }

  cancel(){
    this.dialogRef.close();
  }

  addStep(){
    this.steps.push({subject: this.step.subject, completed: this.step.completed});
  }

  deleteStep(step: any){
    const index: number = this.steps.indexOf(step)
    this.steps.splice(index, 1)
  }

  onFileChange(event : any) {
    const maxAllowedSize = 4 * 1024 * 1024;
    if (event.target.files[0].size > maxAllowedSize){
      alert("You're file is too big, please choose another file!")
    }
    this.currentFile = event.target.files[0];
  }

}
