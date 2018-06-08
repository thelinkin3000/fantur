import { TestBed, inject } from '@angular/core/testing';

import { HotelesService } from './hoteles.service';

describe('HotelesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HotelesService]
    });
  });

  it('should be created', inject([HotelesService], (service: HotelesService) => {
    expect(service).toBeTruthy();
  }));
});
