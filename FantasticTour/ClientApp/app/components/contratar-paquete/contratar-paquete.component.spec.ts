import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContratarPaqueteComponent } from './contratar-paquete.component';

describe('ContratarPaqueteComponent', () => {
  let component: ContratarPaqueteComponent;
  let fixture: ComponentFixture<ContratarPaqueteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContratarPaqueteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContratarPaqueteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
