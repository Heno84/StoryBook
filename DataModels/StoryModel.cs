using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class StoryModel
    {
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(200)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(5000)]
        public string Content { get; set; }

        public string AuthorName { get; set; }

        public DateTime PostedOn { get; set; }

        public IEnumerable<GroupModel> InGroups { get; set; }
        public IEnumerable<GroupModel> NotInGroups { get; set; }
    }
}
