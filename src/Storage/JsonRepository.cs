using Newtonsoft.Json;
using System.IO;

namespace tetrix.Storage
{
    class JsonRepository
    {
        public void Save(SavableData savableData)
        {
            using (StringWriter writer = new StringWriter())
            {
                JsonSerializer.CreateDefault().Serialize(writer, savableData, typeof(SavableData));
                File.WriteAllText("saved.json", writer.GetStringBuilder().ToString());
            }
        }

        public SavableData Load()
        {
            var data = File.ReadAllText("saved.json");
            using (StringReader reader = new StringReader(data))
            {
                var savableData = JsonSerializer.CreateDefault().Deserialize(reader, typeof(SavableData)) as SavableData;
                return savableData;
            }
        }
    }
}
