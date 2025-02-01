import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogactivityComponent } from './logactivity.component';

describe('LogactivityComponent', () => {
  let component: LogactivityComponent;
  let fixture: ComponentFixture<LogactivityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogactivityComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LogactivityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
