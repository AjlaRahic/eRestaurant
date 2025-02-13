import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkladisteComponent } from './skladiste.component';

describe('SkladisteComponent', () => {
  let component: SkladisteComponent;
  let fixture: ComponentFixture<SkladisteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SkladisteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SkladisteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
