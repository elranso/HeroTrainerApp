/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TrainingService } from './Training.service';

describe('Service: Training', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TrainingService]
    });
  });

  it('should ...', inject([TrainingService], (service: TrainingService) => {
    expect(service).toBeTruthy();
  }));
});
