using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Key]
    public string email { get; set; }
    public string name { get; set; }
    public DateTime initialDate { get; set; }
    public DateTime endDate { get; set; }

    public UserModel(string email, string name, DateTime initialDate, DateTime endDate)
    {
        this.email = email;
        this.name = name;
        this.initialDate = initialDate;
        this.endDate = endDate;
    }
}
