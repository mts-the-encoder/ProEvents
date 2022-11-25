import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

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

  constructor(private router: Router) { }

  ngOnInit(): void { }

  toList(): void {
    this.router.navigate([`/${this.title?.toLocaleLowerCase()}/list`]);
  }
}
