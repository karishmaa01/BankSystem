using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HttpClientDemo.Model
{
    public class Register
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }

    }
}
