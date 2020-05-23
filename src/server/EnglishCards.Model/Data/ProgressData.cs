using System.Collections.Generic;

namespace EnglishCards.Model.Data
{
    public class ProgressData : BaseEntity
    {
        public Language Language { get; set; }

        public ICollection<ProgressDataWord> WordsProgress { get; set; } = new List<ProgressDataWord>();
    }
}
