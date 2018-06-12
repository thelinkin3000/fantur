import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TransporteEditorComponent } from './transporte-editor.component';

describe('TransporteEditorComponent', () => {
  let component: TransporteEditorComponent;
  let fixture: ComponentFixture<TransporteEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TransporteEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TransporteEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
