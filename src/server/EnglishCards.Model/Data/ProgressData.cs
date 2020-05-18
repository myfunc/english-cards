using System.Collections.Generic;

namespace EnglishCards.Model.Data
{
    public class ProgressData : BaseEntity
    {
        public ICollection<ProgressDataWord> WordsProgress { get; set; }
    }
}
