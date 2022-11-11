import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BlockBlobClient } from '@azure/storage-blob';
import { IAssignment } from '../model/assignment';
import { AssignmentDetailsService } from './assignment-details.service';
import { saveAs } from 'file-saver' 

@Injectable({
  providedIn: 'root'
})
export class AttachmentService {
  attachment!: any;
  attachmentId!: string;

  constructor(private _http: HttpClient, private _assignemntService : AssignmentDetailsService) { }

  getSasToken(name: string): any {
    return this._http.post<any>('https://localhost:7153/Attachment', { "name": name });
  }

  getSasTokenDownload(id: string): any {
    return this._http.get<any>('https://localhost:7153/Attachment/' + id);
  }

  uploadFile(name: string, file: File, assignment: IAssignment) : IAssignment {
    this.getSasToken(name)
      .subscribe(async (response: any) => {
        this.attachmentId = response.result.attachmentId;
        const client = new BlockBlobClient(response.result.sasToken);
        client.uploadData(file, {
          blobHTTPHeaders: { blobContentType: file.type }
        })
        this._assignemntService.changeAssignment(assignment, this.attachmentId)
          .subscribe( response => {
            this.attachment = response;
          }
        )
      }
    );
    return this.attachment;
  }

  downloadFile(idAttachment : string, fileName : string){
    this.getSasTokenDownload(idAttachment)
    .subscribe(
      (response: any) => {
        const client = new BlockBlobClient(response.result.sasToken);
        client.download().then(
          async result => {
            const blob: any = await result.blobBody;
            saveAs(blob, fileName)
          }
        )
      } 
    )
  }

  deleteAttachment(idAttachment: string){
    return this._http.delete('https://localhost:7153/Attachment/' + idAttachment);
  }
}
