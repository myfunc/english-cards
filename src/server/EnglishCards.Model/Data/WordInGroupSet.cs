using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Model.Data
{
    public class WordInGroupSet
    {
        public Guid WordId { get; set; }

        public Guid GroupSetId { get; set; }

        public Word Word { get; set; }

        public GroupSet GroupSet { get; set; }
    }
}
