namespace Blog.Domain.Entities
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }

        // klucz obcy
        public int UserId { get; set; }
        // właściwość nawigacyjna
        public User User { get; set; }
    }
}
