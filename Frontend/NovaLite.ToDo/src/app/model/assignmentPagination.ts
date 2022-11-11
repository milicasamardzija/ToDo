import { IAssignment } from "./assignment";

export interface IAssignmentPagaination{
    assignments: IAssignment[];
    totalAssignments: number;
}