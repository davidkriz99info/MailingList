using System.ComponentModel.DataAnnotations;

namespace MailingListApi.Requests
{
    public class EntryRequest
    {
        [Required(ErrorMessage = "Required")]
        public required string EmailAddress { get; set; }

        [Required(ErrorMessage = "Required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        public required string LastName { get; set; }
    }
}
