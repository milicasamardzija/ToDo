import { IAttachment } from "./attachment";

export interface IAssignment {
    number: number,
    subject: string ,
    description: string,
    reminder: Date,
    isExpired: boolean,
    attachment: IAttachment
}