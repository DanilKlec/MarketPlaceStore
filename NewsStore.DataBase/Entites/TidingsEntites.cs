using NewsStore.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsStore.DataBase.Entites
{
    public class TidingsEntites
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int? Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateExpiration { get; set; }
        public Guid? UsersId { get; set; }
        public UsersEntites? Users { get; set; }
        [Required]
        [Display(Name = "Image")]
        public string Picture { get; set; }
    }
}
