import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "environments/environment";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class ReportaService {
  
  constructor(private htpp: HttpClient) { }

  public salvar(form): Observable<any>{
    return this.htpp.post(`${environment.apiUrl}Reporta`, form);
  }

}
