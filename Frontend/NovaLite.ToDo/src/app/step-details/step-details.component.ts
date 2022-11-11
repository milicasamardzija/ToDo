import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IStep } from '../model/step';
import { StepService } from './step.service';

@Component({
  selector: 'app-step-details',
  templateUrl: './step-details.component.html',
  styleUrls: ['./step-details.component.css']
})
export class StepDetailsComponent implements OnInit {
  step!: IStep;
  assignmentNumber!: number
  steps!: IStep[];
  update: boolean = true;

  constructor(public dialogRef: MatDialogRef<StepDetailsComponent>, 
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any,
    private _stepService : StepService
    ) {
      this.step = { number: data.number, subject: data.subject, completed: data.completed};
      this.assignmentNumber = data.assignmentNumber;
      this.update = data.update;
     }

  ngOnInit() {
    this.dialogRef.updatePosition({ top: `30px`,
    right: `500px`});
  }

  changeStep(){
    if (this.update == true){
      this._stepService.changeStep(this.step, this.assignmentNumber).subscribe(
      response => {
        this.dialogRef.close({
          data: "updated"
        })
      })
    } else {
      this._stepService.addStep(this.step, this.assignmentNumber).subscribe(
        response => {
          this.dialogRef.close({
            data: "updated"
          })
        })
    }
  }

  deleteStep(){
    if(confirm("Are you sure to delete this step?")){
      return this._stepService.deleteStep(this.step.number, this.assignmentNumber).subscribe(
      response =>{
        this.dialogRef.close({
          data: "updated"
        })
      }
    )
    } else {
      return;
    }
  }
}
