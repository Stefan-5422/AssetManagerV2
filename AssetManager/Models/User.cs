using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssetManager.Models;

/// <summary>
/// The database user object
/// </summary>
public class User
{
    /// <summary>
    /// The primary database Id
    /// </summary>
    [Key]
    [JsonIgnore]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// The username
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Definitly not the plain text password the user entered *cough cough*
    /// TODO: Fix that maybe
    /// </summary>
    [Required]
    [JsonIgnore]
    public string Password { get; set; }
}