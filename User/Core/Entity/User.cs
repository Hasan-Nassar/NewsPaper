using System.ComponentModel.DataAnnotations;

namespace User.Core.Entity
{
    public class User
    {
        
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Password { get; set; }
    }
}
