import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HotelEditorComponent } from './hotel-editor.component';

describe('HotelEditorComponent', () => {
  let component: HotelEditorComponent;
  let fixture: ComponentFixture<HotelEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HotelEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HotelEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
