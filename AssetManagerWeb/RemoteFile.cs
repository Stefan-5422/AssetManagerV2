using System.Text.Json.Serialization;

namespace AssetManagerWeb
{
	public class RemoteFile
	{
		[JsonPropertyName("name")]
		public string FileName { get; set; } = string.Empty;

		[JsonPropertyName("guid")]
		public string RemoteName { get; set; } = string.Empty;
	}
}