namespace PlanetariumApp.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string AnswerA { get; set; } = string.Empty;
        public string AnswerB { get; set; } = string.Empty;
        public string AnswerC { get; set; } = string.Empty;
        public string AnswerD { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty; // 'A', 'B', 'C' або 'D'
    }
}