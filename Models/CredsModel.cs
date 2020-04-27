using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

public class CredsModel
{
    public CredsModel() { }
    
    //[Required]
    //[EmailAddress]
    public string Email { get; set; }

    [Required]
    // [IgnoreDataMember]
    //[DataType(DataType.Password)]        
    public string Password { get; set; } 

    public bool RememberMe { get; set; }
}