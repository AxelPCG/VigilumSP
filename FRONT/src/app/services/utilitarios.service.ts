import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class UtilitariosService {

  constructor() { }

  public exibeAlerta(tipoAlerta: string, mensagem){
    switch(tipoAlerta) {
      case "SUCESSO":
        Swal.fire({
          icon: "success",
          showConfirmButton: true,
          title: "SUCESSO",
          text: mensagem
        })
    }
  }

  public showLoading(){
    Swal.fire({
      allowOutsideClick: false,
      showConfirmButton: false
    });
  }

  public hideLoading(){
    Swal.hideLoading();
    Swal.clickConfirm();
  }
}
