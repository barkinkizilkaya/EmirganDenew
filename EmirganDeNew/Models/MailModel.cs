using System;
using System.ComponentModel.DataAnnotations;

namespace EmirganDeNew.Models
{
    public class MailModel
    {
      public string NameSurname { get; set; }
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
           ErrorMessage = "Bitte tragen Sie Ihre vollen email ein!")]
        [Required]
        public string Email { get; set; }
      public string TelephoneNumber { get; set; }
      public string Topic { get; set; }
      public string Message { get; set; }
    }
}
