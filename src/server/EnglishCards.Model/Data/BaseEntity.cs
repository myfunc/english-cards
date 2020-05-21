using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnglishCards.Model.Data
{
    public class DateObject
    {
        //[SqlDefaultValue(DefaultValue = "now()")]
        public DateTime CreatedOn { get; set; }
    }

    public class BaseEntity : DateObject
    {
        [Key]
        public Guid Id { get; set; }

        public string Tags { get; set; } = string.Empty;
    }
}
