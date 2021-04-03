using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public byte[] Password { get; set; }
    }
}
