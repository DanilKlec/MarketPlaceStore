using NewsStore.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace NewsStore.DataBase.Entites
{
    public class UsersEntites
    {
        [Required]
        [DataMember(Name = "key")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        [ForeignKey("Role")]
        public Guid? RoleId { get; set; }

        [IgnoreDataMember]
        public RoleEntites Role { get; set; }

        [IgnoreDataMember]
        public List<TidingsEntites> Tidings { get; set; }
    }
}
