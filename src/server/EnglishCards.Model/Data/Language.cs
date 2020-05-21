using System.ComponentModel.DataAnnotations;

namespace EnglishCards.Model.Data
{
    public class Language
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
