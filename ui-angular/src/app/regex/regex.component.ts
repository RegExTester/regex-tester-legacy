import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { RegExTesterResult, Match } from '../../model/regextesterresult.model';
import { EncodeUriHelper } from '../../utils/encodeUriBeauty';
import { HighlightTag } from 'angular-text-input-highlight';
import { CONFIG } from './regex.config';

@Component({
  selector: 'app-regex',
  templateUrl: './regex.component.html',
  styleUrls: ['./regex.component.css'],
  providers: [ EncodeUriHelper ]
})
export class RegexComponent implements OnInit, OnDestroy {
  sub;
  delay;
  busy = false;

  engine = '';
  pattern = '';
  text = '';
  options = Object.values(CONFIG.REGEX_OPTIONS).map(opt => ({
    name: opt.Name, value: opt.Value, checked: false
  }));

  matches = '';
  highlight: HighlightTag[] = [];

  constructor(private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private encoder: EncodeUriHelper) {
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      const optionsValue = +params['options'];

      this.pattern = this.encoder.decodeBase64(params['pattern'] || '');
      this.text = this.encoder.decodeBase64(params['text'] || '');
      this.engine = this.encoder.decodeBase64(params['engine'] || '');
      this.options.forEach(opt => {
        opt.checked = (optionsValue & opt.value) === opt.value;
      });

      this.submitRegEx();
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  // debounce user input.
  onModelChanged() {
    this.highlight = [];

    if (this.delay !== null) {
      clearTimeout(this.delay);
    }
    this.delay = setTimeout(() => {
        this.submitRegEx();
    }, CONFIG.DELAY_TIME);
  }

  submitRegEx() {
    this.busy = true;

    let optionsValue = 0;
    this.options.forEach(i => optionsValue += i.checked ? i.value : 0);

    const pattern = this.encoder.encodeBase64(this.pattern || ''),
      text = this.encoder.encodeBase64(this.text || ''),
      engine = this.encoder.encodeBase64(this.engine || ''),
      options = optionsValue.toString();

    if (pattern && text) {
      this.router.navigate([pattern, text, options || '', engine || '']);

      this.http.post<RegExTesterResult>(CONFIG.API.DOTNET.POST, {
        pattern: this.pattern,
        text: this.text,
        options: optionsValue
      }).subscribe(data  => {
        this.matches = JSON.stringify(data);

        this.highlight = [];
        let matchIndex = 0;
        data.matches.forEach(match => {
          this.highlight.push({
            cssClass: 'match-' + (matchIndex++ % CONFIG.MATCH_COLORS_COUNT), indices: {start: match.index, end: match.index + match.length}
          });
        });
      });
    }

    this.busy = false;
  }
}
