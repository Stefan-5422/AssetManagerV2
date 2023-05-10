using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssetManager.Models;

/// <summary>
/// Describes the file object which is stored inside the database
/// </summary>
public class File
{
    /// <summary>
    /// The guid the document is to be referenced by
    /// </summary>
    [Required]
    public Guid Guid { get; set; } = Guid.NewGuid();

    /// <summary>
    /// The primary database Id
    /// Do not touch :)
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// The computed md5 checksum
    /// </summary>
    [Required]
    public string Md5 { get; set; }

    /// <summary>
    /// The name of the file
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// The uploader
    /// </summary>
    [Required]
    public User User { get; set; }
}