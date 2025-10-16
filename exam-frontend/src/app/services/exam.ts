import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

//interface questions
export interface Question {
  id: number;
  text: string;
  answerA: string;
  answerB: string;
  answerC: string;
  answerD: string;
}

//interface answer
export interface AnswerSubmit {
  questionId: number;
  userAnswer: string;
}

//interface exam submit
export interface ExamSubmitRequest {
  name: string;
  answers: AnswerSubmit[];
}

//interface exam result
export interface ExamResult {
  name: string;
  score: number;
}

@Injectable({
  providedIn: 'root'
})
export class ExamService {
    private apiUrl = 'http://localhost:5154';
    constructor(private http: HttpClient) { }

    //fetch questions from backend
    getQuestions(): Observable<Question[]> {
        return this.http.get<Question[]>(`${this.apiUrl}/questions`);
    }
    //submit exam answers to backend
    submitExam(examSubmit: ExamSubmitRequest): Observable<ExamResult> {
        return this.http.post<ExamResult>(`${this.apiUrl}/submit`, examSubmit);
    }
}
