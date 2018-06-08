import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaqueteEditorComponent } from './paquete-editor.component';

describe('PaqueteEditorComponent', () => {
  let component: PaqueteEditorComponent;
  let fixture: ComponentFixture<PaqueteEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaqueteEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaqueteEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
