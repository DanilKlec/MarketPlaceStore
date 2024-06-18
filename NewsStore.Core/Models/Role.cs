using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NewsStore.Core.Models
{
    public class Role
    {
        [Required]
        [DataMember(Name = "key")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<Users> Users { get; set; }
        public Role()
        {
            Users = new List<Users>();
        }
    }
}
