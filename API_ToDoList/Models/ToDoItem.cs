using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DefaultNamespace;

[Table("ToDoItens")]   
public class ToDoItem
{   
    [Key]
    public int ToDoItemId { get; set; }
    
    [Required]
    [MaxLength(80)]
    public string name { get; set; }
    
    public string content { get; set; }
    
    [Required]
    public bool is_done { get; set; }
    
    [Required]
    public DateTime created_at { get; set; }
    
    [Required]
    public DateTime updated_at { get; set; }

    [Required]
    public int userId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }

}