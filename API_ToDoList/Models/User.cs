using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DefaultNamespace;

[Table("Users")]
public class User
{
    [Key]
    public int userId { get; set; }
    
    [Required]
    [MaxLength(80)]
    public string name {get; set;}
    
    [Required]
    [MaxLength(80)]
    public string email {get; set;}
    
    [Required]
    [MaxLength(80)]
    public string password {get; set;}
    
    [Required]
    public DateTime created_at {get; set;}
    
    [Required]
    public DateTime updated_at {get; set;}

    public ICollection<ToDoItem> todo_items {get; set;}
}