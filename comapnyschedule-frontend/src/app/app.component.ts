import { Component } from '@angular/core';
import { DataServciceClient, Schedule } from './app.data.service.client';
import { TimelineItem } from 'ngx-horizontal-timeline';
import { MomentModule } from 'ngx-moment';
import * as moment from 'moment';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [DataServciceClient]
})
export class AppComponent {
  title = 'comapnyschedule-frontend';
  schedules: Schedule[] = [];
  things: TimelineItem[] = [];
  error = undefined;

  constructor(private dataProvider: DataServciceClient) {
    this.getSchedules();
  }

  getSchedules() {
    this.dataProvider.getSchedule()
    .subscribe(data => this.schedules = data,
      error => this.error = error)
  }

  isInPast(date: string)
  {
    var start = moment(date, 'DD/MM/YYYY');
    var end = moment(new Date());
    const diffDays = end.diff(start);

    return diffDays > 0;
  }

  isFuture(date: string)
  {
    var start = moment(date, 'DD/MM/YYYY');
    var end = moment(new Date());
    const diffDays = end.diff(start);

    return diffDays <= 0;
  }

  calculateDistance(date: string, item: Schedule)
  {
    const itemIndex = item.schedule.indexOf(date);
    console.log(itemIndex);
    console.log(item.schedule.length);
    if (itemIndex + 1 < item.schedule.length)
    {
      var start = moment(date, 'DD/MM/YYYY');
      var end = moment(item.schedule[itemIndex + 1], 'DD/MM/YYYY');
      const diffDays = end.diff(start, 'days');

      return `----${diffDays}----`
    }
    return '';
  }


}
