using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FootballStats.Models;

public class PlayerModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Lastname { get; set; }
    public string Firstname { get; set; }
    public string Middlename { get; set; }

    [NotMapped]
    public string Fullname => $"{Lastname} {Firstname} {Middlename}";
}