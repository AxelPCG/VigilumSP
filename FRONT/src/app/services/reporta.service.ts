import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "environments/environment";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class ReportaService {
  
  constructor(private htpp: HttpClient) { }

  private url = "Ocorrencia";

  public salvar(form): Observable<any>{
    return this.htpp.post<any>(`${environment.apiUrl}Ocorrencia`, form, {
      headers: null
    });
  }

}
