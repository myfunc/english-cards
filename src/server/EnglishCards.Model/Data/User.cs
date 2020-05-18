using System.Collections.Generic;

namespace EnglishCards.Model.Data
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public ICollection<UserInGroup> UserInGroup { get; set; } = new List<UserInGroup>();

        public ProgressData ProgressData { get; set; }
    }
}
