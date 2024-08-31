import { ReportaService } from './../services/reporta.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UtilitariosService } from 'app/services/utilitarios.service';
import { catchError } from 'rxjs';

@Component({
  selector: 'app-reportar',
  templateUrl: './reportar.component.html',
  styleUrls: ['./reportar.component.scss']
})
export class ReportarComponent implements OnInit {

  constructor(
    private fb: FormBuilder,
    private reportaService: ReportaService,
    private utilitariosService: UtilitariosService,
    private router: Router) { }

  public form: FormGroup;

  ngOnInit() {
    this.createForm();
  }

  private createForm(){
    this.form = this.fb.group({
      primeiroNome: [{ value: null, disabled: false}, [Validators.required]],
      sobrenome: [{ value: null, disabled: false}, [Validators.required]],
      email: [{ value: null, disabled: false}, [Validators.required, Validators.email]],
      telefone: [{ value: null, disabled: false}, [Validators.required]],
      assunto: [{ value: null, disabled: false}, [Validators.required]],
      descricao: [{ value: null, disabled: false}, [Validators.required]],
      mensagem: [{ value: null, disabled: false}, [Validators.required]],
      concordo: [{ value: false, disabled: false}, [Validators.required]]
    });
  }

  public salvar(){
    console.log("Formulario: ", this.form.value)

    this.utilitariosService.showLoading();

    this.reportaService.salvar(this.form.value)
      .pipe(
        catchError(err => { throw err })
      ).subscribe(response => {
        this.form.reset();
        this.utilitariosService.exibeAlerta('SUCESSO', 'A sua ocorrência foi registrada com sucesso! Muito obrigado por reportar.');
        this.utilitariosService.hideLoading();
      })
  }

}
