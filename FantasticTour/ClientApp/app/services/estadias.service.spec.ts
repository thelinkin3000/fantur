import { TestBed, inject } from '@angular/core/testing';

import { EstadiasService } from './estadias.service';

describe('EstadiasService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EstadiasService]
    });
  });

  it('should be created', inject([EstadiasService], (service: EstadiasService) => {
    expect(service).toBeTruthy();
  }));
});
