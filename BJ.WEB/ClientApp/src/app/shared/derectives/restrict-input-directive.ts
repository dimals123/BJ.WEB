import { Directive, HostListener, ElementRef, Input, Renderer2 } from '@angular/core';

@Directive({
selector: '[appInputMask]'
})
export class RestrictInputDirective {
@Input('appInputMask') inputType: string;

showMsg = false;
pattern: RegExp;

private regexMap = { 
integer: /^[0-9 ]*$/g
};

constructor(public el: ElementRef, public renderer: Renderer2) { };

@HostListener('keypress', ['$event']) onInput(e) {
this.pattern = this.regexMap[this.inputType]
const inputChar = e.key;
this.pattern.lastIndex = 0;

if (this.pattern.test(inputChar)) {
} else {
  e.preventDefault();
}}

  }