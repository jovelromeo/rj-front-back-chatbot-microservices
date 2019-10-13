import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
declare var signalR: any;
@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  constructor(private http: HttpClient) { }
  messages: string[];
  logEmail: string;
  logPassword: string;
  regEmail: string;
  regPassword:string;
  regPasswordConfirm:string;
  token:Token;
  authRequired=true;
  connection:any;
  message:string;
  loggedInEmail:string;
  ngOnInit() {
    this.messages = [];
    this.startHttpRequest();
  }

  private startHttpRequest = () => {
    // this.client.get('https://localhost:44377/chatHub')
    //   .subscribe(res => {
    //     console.log(res);
    //     this.messages.push(res.toString());
    //   })
  }

  logout():void{
    this.token=null;
    this.authRequired=true;
  }

  login(): void {
    this.http.post<Token>(environment.apiUrl+"/api/User/Authenticate", { email: this.logEmail, password: this.logPassword }).subscribe(data => {
      this.token=data;
      this.authRequired=false;
      this.loggedInEmail=this.logEmail;
      this.prepareSignalR();
    }, err=>{
      alert(err.error.message);
        });
  }
  
  register():void{
    this.http.post<Token>(environment.apiUrl+"/api/User/Register", { email: this.regEmail, password: this.regPassword, confirmPassword:this.regPasswordConfirm }).subscribe(data => {
      this.token=data;
      this.authRequired=false;
      this.loggedInEmail=this.logEmail;
      this.prepareSignalR();
    }, err=>{
      console.log(err);
      alert(err.error.join(". "));
    });
  }
  prepareSignalR() {
    this.connection = new signalR.HubConnectionBuilder().withUrl(environment.apiUrl+"/chatHub", {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets
  }).build();

    this.connection.on("ReceiveMessage", function (user, message) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = user + " says " + msg;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

    this.connection.start().then(function () {
        
    }).catch(function (err) {
        return console.error(err.toString());
    });
  }

  sendMessage() {

    var message = this.message.toString();
    this.connection.invoke("SendMessage", this.loggedInEmail, message).catch(function (err) {
        return console.error(err.toString());
    });
    this.message="";
}
}

export interface Token{
  expirationTime:string,token:string,
}
