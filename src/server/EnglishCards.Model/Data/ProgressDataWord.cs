namespace EnglishCards.Model.Data
{
    public class ProgressDataWord : BaseEntity
    {
        public Word Word { get; set; }

        public Language Language { get; set; }

        public int LearnAttempts { get; set; }

        public bool IsLearned { get; set; }
    }
}
