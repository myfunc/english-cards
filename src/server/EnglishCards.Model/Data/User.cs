using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishCards.Model.Data
{
    public class User : BaseEntity
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<UserInGroup> UserInGroup { get; set; } = new List<UserInGroup>();

        public ICollection<ProgressData> ProgressData { get; set; } = new List<ProgressData>();

        [Required]
        public Language NativeLanguage { get; set; }

        public Language ForeignLanguage { get; set; }
    }
}
