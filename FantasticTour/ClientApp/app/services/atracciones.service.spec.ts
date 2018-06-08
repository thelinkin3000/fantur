import { TestBed, inject } from '@angular/core/testing';

import { AtraccionesService } from './atracciones.service';

describe('AtraccionesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AtraccionesService]
    });
  });

  it('should be created', inject([AtraccionesService], (service: AtraccionesService) => {
    expect(service).toBeTruthy();
  }));
});
