using System.IO;
using System.Text.Json;

namespace Tetrix.Storage;

public static class JsonRepository
{
	private const string SAVE_FN = "saved.json";
	public static void Save(SavableData savableData) => File.WriteAllText(SAVE_FN, JsonSerializer.Serialize(savableData));
	public static SavableData Load() => JsonSerializer.Deserialize<SavableData>(File.ReadAllText(SAVE_FN));
}
