using System.Collections.Generic;

namespace EnglishCards.Model.Data
{
    public class Word : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<GroupSet> Groups { get; set; }

        public ICollection<WordInGroupSet> WordInGroupSets { get; set; }
    }
}
