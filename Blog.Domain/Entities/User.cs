using System.Collections.Generic;

namespace Blog.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        // właściwość nawigacyjna
        public ContactInfo ContactInfo { get; set; }

        // użytkownik może mieć też wiele postów, jeden post może być napisany
        // przez jednego użytkownika

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
