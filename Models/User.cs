using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public string email { get; set; }
    public string name { get; set; }
    public DateTime initialDate { get; set; }
    public DateTime endDate { get; set; }

    // Construtor
    public User(string email, string name, DateTime initialDate, DateTime endDate)
    {
        this.email = email;
        this.name = name;
        this.initialDate = initialDate;
        this.endDate = endDate;
    }
}
