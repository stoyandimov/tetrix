using System.Text.Json;

namespace Tetrix.GameEngine.Storage;

public static class JsonFileRepository
{
	private const string SAVE_FN = "saved.json";
	public static void Save(SavableData savableData) => File.WriteAllText(SAVE_FN, JsonSerializer.Serialize(savableData));
	public static SavableData Load() => JsonSerializer.Deserialize<SavableData>(File.ReadAllText(SAVE_FN));
}
