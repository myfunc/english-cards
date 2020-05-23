using System.Collections.Generic;

namespace EnglishCards.Model.Data
{
    public class Word : BaseEntity
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<WordInGroupSet> WordInGroupSets { get; set; } = new List<WordInGroupSet>();

        public ICollection<WordTranslation> WordTranslations { get; set; } = new List<WordTranslation>();
    }
}
