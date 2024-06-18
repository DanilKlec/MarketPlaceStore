using NewsStore.Core.Models;
using System.Security.Claims;

namespace NewsStore.API.ViewModels
{
    public class TidingsListingModel
    {
        public List<Tidings> News { get; set; }
        public Users User { get; set; }
        public int? Number { get; set; }
        public string Picture { get; set; }
        public string Discription { get; set; }
        public int Rating { get; set; }
        public DateTime DateExpiration { get; set; }
        public bool IsAdmin { get; set; }
    }
}
