import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VerPaqueteComponent } from './ver-paquete.component';

describe('VerPaqueteComponent', () => {
  let component: VerPaqueteComponent;
  let fixture: ComponentFixture<VerPaqueteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VerPaqueteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VerPaqueteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
