
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import moment from 'moment';

export interface ResponseMetaData {
  status: number;
  message: any;
  isError: boolean;
  errorDetails: any;
  correaltionId: any;
  result: Result[];
}

export interface Result {
  key: string
  value: number
}
@Component({
  selector: 'app-access-per-webserver',
  templateUrl: './access-per-webserver.component.html',
  styleUrl: './access-per-webserver.component.css'
})
export class AccessPerWebserverComponent {
  
  selectedStartDate?: Date;
  selectedStartTime?: Date;
  selectedEndDate?: Date;
  selectedEndTime?: Date;

  public responseMetaData: ResponseMetaData = {
    status: 0,
    message: undefined,
    isError: false,
    errorDetails: undefined,
    correaltionId: undefined,
    result: []
  };

  constructor(private http: HttpClient) {
    this.selectedStartDate = new Date();
    this.selectedStartTime = new Date();
    this.selectedEndDate = new Date();
    this.selectedEndTime = new Date();
  }

  ngOnInit() {
  }


  uploadFile(file: File) {
    const formData = new FormData();
    formData.append('file', file);
    var formattedStartDateTime = moment(this.selectedStartDate).format('MM/DD/YYYY') + ' ' + moment(this.selectedStartTime).format('hh:mm A');
    var formattedEndDateTime = moment(this.selectedEndDate).format('MM/DD/YYYY') + ' ' + moment(this.selectedEndTime).format('hh:mm A');

    this.http.post<ResponseMetaData>('https://localhost:7154/api/v1/parse/access-per-host?startDate=' + formattedStartDateTime + '&endDate=' + formattedEndDateTime, formData).subscribe(
      (result) => {
        this.responseMetaData = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

}
