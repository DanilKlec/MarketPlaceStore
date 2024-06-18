using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Runtime.Serialization;

namespace NewsStore.Core.Models
{
    public class Users
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
        public Role? Role { get; set; }
        public static (Users Users, string Error) Create(Guid id, string userName, string name, string password, Guid roleId)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(userName))
            {
                error = "Необходимо заполнить имя пользователя";
            }

            var users = new Users()
            {
                Id = id,
                UserName = userName,
                Name = name,
                Password = password,
                RoleId = roleId,
            };

            return (users, error);
        }
    }
}
