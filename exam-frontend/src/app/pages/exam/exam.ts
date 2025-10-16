import { Component } from '@angular/core';
import { ExamService, Question, ExamSubmitRequest, ExamResult } from '../../services/exam';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-exam',
  imports: [FormsModule, CommonModule],
  templateUrl: './exam.html',
  styleUrl: './exam.css'
})

export class ExamComponent  {
  questions: Question[] = [];
  name: string = '';
  answers: Record<number, string> = {};
  result: ExamResult | null = null;
  isSubmitted: boolean = false;

  constructor(private examService: ExamService) { }
  
  ngOnInit(): void {
    this.loadQuestions();}
  
  loadQuestions() {
    this.examService.getQuestions().subscribe(data =>
      {this.questions = data;});
  }

  onSubmit() {
    if(!this.name) {
      alert('กรุณากรอกชื่อของคุณ');
      return;
    }

    const submitData: ExamSubmitRequest = {
      name: this.name,
      answers: this.questions.map(q => ({
        questionId: q.id,
        userAnswer: this.answers[q.id] || ''
      }))
    };

    this.examService.submitExam(submitData).subscribe(res => {
      this.result = res;
      this.isSubmitted = true;
    });
  }

  resetExam() {
    this.name = '';
    this.answers = {};
    this.isSubmitted = false;
    this.loadQuestions();
  }

}
