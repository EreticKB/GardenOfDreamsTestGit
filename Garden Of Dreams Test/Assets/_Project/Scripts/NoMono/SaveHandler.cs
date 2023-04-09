using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveHandler
{
    private static string SerializeObject(object obj)
    {
        if (!obj.GetType().IsSerializable)
        {
            return null;
        }
        using (MemoryStream stream = new MemoryStream())
        {
            new BinaryFormatter().Serialize(stream, obj);
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    private static InventoryDataCollector DeserializeObject(string str)
    {
        byte[] bytes = Convert.FromBase64String(str);
        using (MemoryStream stream = new MemoryStream(bytes))
        {
            return (InventoryDataCollector)new BinaryFormatter().Deserialize(stream);
        }
    }

    public static void Save(InventoryDataCollector file, string filename)
    {
        File.WriteAllText(filename, SerializeObject(file));
    }

    public static bool Load(string filename, out InventoryDataCollector data)
    {
        if (!File.Exists(filename))
        {
            data = new InventoryDataCollector();
            return false;
        }
        data = DeserializeObject(File.ReadAllText(filename));
        return true;
    }
}
