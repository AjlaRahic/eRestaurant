import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PregledulazaComponent } from './pregledulaza.component';

describe('PregledulazaComponent', () => {
  let component: PregledulazaComponent;
  let fixture: ComponentFixture<PregledulazaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PregledulazaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PregledulazaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
