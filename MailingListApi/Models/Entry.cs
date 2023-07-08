namespace MailingListApi.Models
{
    public class Entry
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public required string EmailAddress { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
