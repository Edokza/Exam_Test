import { Component, signal } from '@angular/core';
import { ExamComponent } from './pages/exam/exam';

@Component({
  selector: 'app-root',
  imports: [ExamComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('exam-frontend');
}
