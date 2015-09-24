using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
  public  class GroupModel
    {
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(200)]
        public string Description { get; set; }

        public string OwnerName { get; set; }

        public int MembersCount { get; set; }

        public int StoriesCount { get; set; }

        public string ByLine { get; set; }

        public bool AmIOwnerOf { get; set; }
        
        public bool AmIMemberOf { get; set; }

        public IEnumerable<string> Members { get; set; }
    }
}
