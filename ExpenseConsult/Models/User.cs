using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace ExpenseConsult.Models;

public class User : IdentityUser
{
    //[Key][JsonIgnore]
    //public int Id { get; set; }

    //[Required]
    //[MaxLength(100)]
    //public string Username { get; set; }

    //[EmailAddress]
    //public string Email { get; set; }

    //private string PasswordHash { get; set; }

    [JsonIgnore]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    //public User(string email, string password)
    //{
    //    Email = email;
    //    PasswordHash = HashPassword(password);
    //}

    //public bool VerifyPassword(string password)
    //{
    //    return HashPassword(password) == PasswordHash;
    //}

    //private string HashPassword(string password)
    //{
    //    using (SHA256 sha256 = SHA256.Create())
    //    {
    //        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
    //        StringBuilder builder = new StringBuilder();
    //        foreach (byte b in bytes)
    //        {
    //            builder.Append(b.ToString("x2"));
    //        }
    //        return builder.ToString();
    //    }
    //}
}
