import { TestBed, inject } from '@angular/core/testing';

import { PaquetesService } from './paquetes.service';

describe('PaquetesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PaquetesService]
    });
  });

  it('should be created', inject([PaquetesService], (service: PaquetesService) => {
    expect(service).toBeTruthy();
  }));
});
