import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.scss']
})
export class TitleComponent implements OnInit {
  @Input() title: string | undefined;
  @Input() subTitle = 'Since 2022';
  @Input() iconClass = 'fa fa-user';
  @Input() buttonList: boolean | undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
