import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { IAssignment } from '../model/assignment';

@Injectable({
  providedIn: 'root'
})
export class AssignmentDetailsService {

  constructor(private _http: HttpClient) { }

  getAssignmets(pageNumber : number, pageSize: number){
    return this._http.get<any>('https://localhost:7153/Assignment?pageNumber=' + pageNumber + '&pageSize=' + pageSize)
  }

  getAssignment(assigmentNumber: number){
    return this._http.get<any>('https://localhost:7153/Assignment/' + assigmentNumber);
  }

  deleteAssignment(assigmentNumber: number){
    return this._http.delete('https://localhost:7153/Assignment/' + assigmentNumber);
  }

  changeAssignment(assignment: IAssignment, attachmentId: string){
    return this._http.put<IAssignment>('https://localhost:7153/Assignment', { "number": assignment.number, "subject": assignment.subject, "description": assignment.description, "reminder": assignment.reminder, "IdAttachment" : attachmentId});
  }

  addAssignment(assignment: IAssignment, steps: any[]){
    return this._http.post<any>('https://localhost:7153/Assignment', {  "subject": assignment.subject, "description" : assignment.description, "reminder" : assignment.reminder, "steps": steps} );
  }

}
