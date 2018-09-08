import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { AlertifyService } from '../services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  constructor(private auth: AuthService, private alert: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.auth.register(this.model).subscribe(() => {
      this.alert.success('registration successful');
    }, err => {
      this.alert.error(err);
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
    this.alert.message('Registration Cancelled!');
  }

}
