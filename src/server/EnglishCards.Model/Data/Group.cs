using System.Collections.Generic;

namespace EnglishCards.Model.Data
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<UserInGroup> UserInGroup { get; set; } = new List<UserInGroup>();
    }
}
