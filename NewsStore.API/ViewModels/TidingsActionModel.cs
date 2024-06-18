using NewsStore.Core.Models;

namespace NewsStore.API.ViewModels
{
    public class TidingsActionModel : EditImageModel
    {
        public Guid Id { get; set; }
        public string TidingsId { get; set; }
        public int? Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public int Rating { get; set; }
        public DateTime DateExpiration { get; set; }
        public Guid? UsersId { get; set; }
        public Users? Users { get; set; }

        public TidingsActionModel()
        {
            DateAdded = DateTime.Now;
        }
    }
}
