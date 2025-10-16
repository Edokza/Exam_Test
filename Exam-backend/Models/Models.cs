namespace Exam_backend.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = "";

        public string AnswerA { get; set; } = "";
        public string AnswerB { get; set; } = "";
        public string AnswerC { get; set; } = "";
        public string AnswerD { get; set; } = "";

        public string CorrectAnswer { get; set; } = "";
    }


    public class ExamResult
    {
        public int Id { get; set; }
        public string ExaminerName { get; set; }
        public int Score { get; set; }
    }

    //ForFrontend
    public class ExamSubmitRequest
    {
        public string Name { get; set; } = "";
        public List<AnswerSubmit> Answers { get; set; } = new();
    }

    public class AnswerSubmit
    {
        public int QuestionId { get; set; }
        public string UserAnswer { get; set; } = ""; // "A","B","C","D"
    }

}
