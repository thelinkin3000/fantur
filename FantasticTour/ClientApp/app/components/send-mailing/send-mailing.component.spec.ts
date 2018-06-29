import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SendMailingComponent } from './send-mailing.component';

describe('SendMailingComponent', () => {
  let component: SendMailingComponent;
  let fixture: ComponentFixture<SendMailingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SendMailingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SendMailingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
