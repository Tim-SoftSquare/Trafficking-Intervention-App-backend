using System.ComponentModel.DataAnnotations;

namespace Trafficking_Intervention_backend {

    public class User {

        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}