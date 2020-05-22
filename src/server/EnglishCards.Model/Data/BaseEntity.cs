using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EnglishCards.Model.Data
{
    public class DateObject
    {
        public DateTime CreatedOn { get; set; }
    }

    public class BaseEntity : DateObject
    {
        [Key]
        public Guid Id { get; set; }

        public string Tags { get; set; } = string.Empty;

        public bool HasTag(string tag)
        {
            return GetTags().Contains(tag);
        }

        public void AddTag(string tag)
        {
            if (!HasTag(tag))
            {
                var newTags = GetTags().ToList();
                newTags.Add(tag);
                Tags = string.Join(";", newTags);
            }
        }

        public void RemoveTag(string tag)
        {
            if (HasTag(tag))
            {
                var newTags = GetTags().ToList();
                newTags.Remove(tag);
                Tags = string.Join(";", newTags);
            }
        }

        public IEnumerable<string> GetTags()
        {
            return Tags.Split(";");
        }
    }
}
