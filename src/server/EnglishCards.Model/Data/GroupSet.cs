using System.Collections.Generic;

namespace EnglishCards.Model.Data
{
    public class GroupSet : BaseEntity
    {
        public string Name { get; set; }
        public string PreviewImageUrl { get; set; }
        public ICollection<WordInGroupSet> WordInGroupSets { get; set; } = new List<WordInGroupSet>();
    }
}
