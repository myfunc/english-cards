namespace EnglishCards.Model.Data
{
    public class WordTranslation : BaseEntity
    {
        public Language Language { get; set; }

        public Word Word { get; set; }

        public string Translation { get; set; }

        public int Priority { get; set; }
    }
}
