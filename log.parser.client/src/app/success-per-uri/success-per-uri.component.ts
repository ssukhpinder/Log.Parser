
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import moment from 'moment';
import { ResponseMetaData } from '../access-per-webserver/access-per-webserver.component';

export interface Result {
  key: string
  value: number
}

@Component({
  selector: 'app-success-per-uri',
  templateUrl: './success-per-uri.component.html',
  styleUrl: './success-per-uri.component.css'
})
export class SuccessPerUriComponent {

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
    if (file !== undefined) {
      const formData = new FormData();
      formData.append('file', file);
      var formattedStartDateTime = moment(this.selectedStartDate).format('MM/DD/YYYY') + ' ' + moment(this.selectedStartTime).format('hh:mm A');
      var formattedEndDateTime = moment(this.selectedEndDate).format('MM/DD/YYYY') + ' ' + moment(this.selectedEndTime).format('hh:mm A');

      this.http.post<ResponseMetaData>('https://localhost:7154/api/v1/parse/success-per-uri?startDate=' + formattedStartDateTime + '&endDate=' + formattedEndDateTime, formData).subscribe(
        (result) => {
          this.responseMetaData = result;
        },
        (error) => {
          console.error(error);
        }
      );
    }
    else {
      alert("Please upload the log file");
    }
  }
}
