import { TestBed, inject } from '@angular/core/testing';

import { ContratarPaquetesService } from './contratar-paquetes.service';

describe('ContratarPaquetesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ContratarPaquetesService]
    });
  });

  it('should be created', inject([ContratarPaquetesService], (service: ContratarPaquetesService) => {
    expect(service).toBeTruthy();
  }));
});
