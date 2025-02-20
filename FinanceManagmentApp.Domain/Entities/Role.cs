namespace FinanceManagmentApp.Domain.Entities
{
    public sealed class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<User> Users { get; set; } = new List<User>();
    }
}
