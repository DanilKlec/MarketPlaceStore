using NewsStore.Core.Models;

namespace NewsStore.API.ViewModels
{
    public class CabinetModel
    {
        public List<Tidings> News { get; set; }
        public List<UserModel> Users { get; set; }
        public UserModel UserAutorize { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsModer { get; set; }
    }
}
