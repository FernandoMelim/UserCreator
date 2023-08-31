namespace UserCreator.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime? BirthDate { get; set; }

    public int? SchoolingLevel { get; set; }

    public string Phone { get; set; }

    public List<Address> Adresses { get; set; }
}

