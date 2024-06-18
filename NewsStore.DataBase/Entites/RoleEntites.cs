namespace NewsStore.DataBase.Entites
{
    public class RoleEntites
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<UsersEntites> Users { get; set; }
        public RoleEntites()
        {
            Users = new List<UsersEntites>();
        }
    }
}
