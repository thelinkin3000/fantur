import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EstadiaEditorComponent } from './estadia-editor.component';

describe('EstadiaEditorComponent', () => {
  let component: EstadiaEditorComponent;
  let fixture: ComponentFixture<EstadiaEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EstadiaEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EstadiaEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
