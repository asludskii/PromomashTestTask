import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Country } from '../model/country';
import { Province } from '../model/province';
import { FormsModule, FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-signup-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule],
  templateUrl: './signup-form.component.html',
  styleUrl: './signup-form.component.css'
})
export class SignupFormComponent
{
  private getCountriesUrl = 'http://localhost:5197/Countries';
  private getProvincesUrl = "http://localhost:5197/Countries/{countryId}/Provinces";
  private postSignupUrl = "http://localhost:5197/SignUp"

  countries : Country[];
  selectedCountry : Country;
  provinces = new Array<Province>
  formData = this.formBuilder.group({
    login: ['', Validators.required, Validators.email],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
    policyAccepted: ['', Validators.required],
    countryid: ['', Validators.required],
    provinceid: ['', Validators.required]
  }, {Validators: this.validatePasswordFn});

  formStep = 1;

  constructor(
    private httpClient: HttpClient, 
    private formBuilder: FormBuilder,
    private router : Router)
  {
  }

  ngOnInit()
  {
    this.httpClient.get(this.getCountriesUrl).subscribe({
      error: (err) =>
      {
        console.log(err);
      },
      next: (value) =>
      {
        this.countries = value as Array<Country>;
      },
      complete: () =>
      {
      }
    });
  }

  onCountrySelect()
  {
    console.log(this.formData.value);
    const countryId = this.formData.value.countryid;
    
    this.httpClient.get(this.getProvincesUrl.replace('{countryId}', countryId || '')).subscribe({
      error: (err) => 
      {
        console.log(err);
      },
      next: (value) =>
      {
        this.provinces = value as Array<Province>;
      },
    });
  }

  onSubmitClick(){
    if(this.formStep === 1)
    {
      // validate
      console.log('step 1 > 2');
      
      this.formStep = 2;
    }
    else if(this.formStep === 2)
    {
      // validate and send
      console.log(this.formData);
      
      this.httpClient.post(this.postSignupUrl, this.formData.value).subscribe({
        error: (err) =>
        {
          console.log(err);
        },
        next: (value) => 
        {
          console.log(JSON.stringify(value));
        },
      });
    }
  }

  validatePasswordFn(group: any) : any | null 
  {
    let pass1 = group.get('password').value;
    let pass2 = group.get('confirmPassword').value;
    return pass1 === pass2 ? null : {notSame: true};
  };
}
