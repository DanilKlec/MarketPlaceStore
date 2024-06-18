namespace NewsStore.Core.Models
{
    public class Tidings
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int? Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateExpiration { get; set; }
        public Guid? UsersId { get; set; }
        public Users? Users { get; set; }
        public string Picture { get; set; }
        public static (Tidings Tidings, string Error) Create(Guid id, int? number, string description, string name, int rating, Users? users, string picture)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(description))
            {
                error = "Необходимо заполнить описание";
            }

            var tidings = new Tidings()
            {
                Id = id,
                Number = number,
                Name = name,
                Description = description,
                Rating = rating,
                DateCreate = DateTime.Now,
                Users = users,
                Picture = picture
            };

            return (tidings, error);
        }

        public static (Tidings Tidings, string Error) Get(Guid id, int? number, string description, string name, int rating, Guid? usersId, string picture)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(description))
            {
                error = "Необходимо заполнить описание";
            }

            var tidings = new Tidings()
            {
                Id = id,
                Number = number,
                Name = name,
                Description = description,
                Rating = rating,
                DateCreate = DateTime.Now,
                UsersId = usersId,
                Picture = picture
            };

            return (tidings, error);
        }
    }
}
