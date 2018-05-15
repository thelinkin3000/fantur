import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtraccionEditorComponent } from './atraccion-editor.component';

describe('AtraccionEditorComponent', () => {
  let component: AtraccionEditorComponent;
  let fixture: ComponentFixture<AtraccionEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtraccionEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtraccionEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
