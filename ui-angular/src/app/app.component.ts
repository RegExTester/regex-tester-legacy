import {Component} from '@angular/core';
import {HighlightTag} from 'angular-text-input-highlight';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  pattern = '';
  text = 'This is a\n    multiline textarea with highlight.';
  options = 0;

  highlight: HighlightTag[] = [
    {indices: {start: 2, end: 4}, cssClass: 'match-1'},
    {indices: {start: 5, end: 7}, cssClass: 'match-2'},
    {indices: {start: 8, end: 14}, cssClass: 'match-3'},
    {indices: {start: 24, end: 32}, cssClass: 'match-4'},
    {indices: {start: 38, end: 42}, cssClass: 'match-5'}
  ];

  onInputText(innerHTML) {
    this.text = innerHTML
      .replace(/<\s*\/?\s*div\s*>/ig, '\n')
      .replace(/<\s*br\s*\/?\s*>/ig, '\n')
      .replace(/\b\s+\b/ig, '\n')
      .replace(/\n{2,}/ig, '\n');
  }
}
