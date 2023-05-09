using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Models;

public class File
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public long Id { get; set; }
	[Required]
	public required string Name { get; set; }
	[Required]
	public required string Md5 { get; set; }
	[Required]
	public Guid Uuid { get; set; } = Guid.NewGuid();
	//public User User { get; set; } TODO: todo
}