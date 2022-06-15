#pragma warning disable CS8168
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Users
{
    [Key]
    public int UserId {get; set;}
}