import { TestBed, inject } from '@angular/core/testing';

import { CiudadesService } from './ciudades.service';

describe('CiudadesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CiudadesService]
    });
  });

  it('should be created', inject([CiudadesService], (service: CiudadesService) => {
    expect(service).toBeTruthy();
  }));
});
