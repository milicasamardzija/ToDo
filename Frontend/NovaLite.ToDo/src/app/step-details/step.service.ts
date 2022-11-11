import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IStepChange } from '../model/stepChange';
import { IStep } from '../model/step';
import { IStepPagaination } from '../model/stepPagination';
import { IStepAdd } from '../model/stepAdd';

@Injectable({
  providedIn: 'root'
})
export class StepService {

  constructor(private _http: HttpClient) { }

  getSteps(pageNumber : number, pageSize: number, assignmentNumber: number){
    return this._http.get<IStepPagaination>('https://localhost:7153/Steps?pageNumber=' + pageNumber + '&pageSize=' + pageSize + '&assignmentNumber=' + assignmentNumber);
  }

  addStep(step: IStep, assignmentNumber: number){
    return this._http.post<IStepAdd>('https://localhost:7153/Steps', {"assignmentNumber": assignmentNumber, "subject": step.subject, "completed": step.completed})
  }

  changeStep(step: IStep, assignmentNumber: number){
    return this._http.put<IStepChange>('https://localhost:7153/Steps', {"assignmentNumber": assignmentNumber, "number": step.number, "subject": step.subject, "completed": step.completed})
  }

  deleteStep(numberStep: number, numberAssignment: number){
    return this._http.delete<any>('https://localhost:7153/Steps?numberStep=' + numberStep + '&numberAssignment=' + numberAssignment);
  }
}
