/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LotService } from './lot.service';

describe('Service: Lot', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LotService]
    });
  });

  it('should ...', inject([LotService], (service: LotService) => {
    expect(service).toBeTruthy();
  }));
});
