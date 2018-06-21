import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EstadiasComponent } from './estadias.component';

describe('EstadiasComponent', () => {
  let component: EstadiasComponent;
  let fixture: ComponentFixture<EstadiasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EstadiasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EstadiasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
