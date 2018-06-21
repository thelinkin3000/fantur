import { Component, Input, OnInit } from '@angular/core';

import { NgbActiveModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'error-modal',
    templateUrl: './error-modal.component.html'
})
export class ErrorModalComponent {
    closeResult: string;
    public errorMessage: string;
    public title: string;

    constructor(public activeModal: NgbActiveModal) {
        console.log(this.errorMessage);
    }

    ngOnInit() {
        console.log("OnInit" + this.errorMessage);
    }

    closeModal() {
        this.activeModal.close('Modal Closed');
    }
}