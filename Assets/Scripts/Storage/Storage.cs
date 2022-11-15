using System.IO;
using UnityEngine;

public class Storage : Singleton<Storage>
{
    public static string StorageDirectory => Path.Combine(Application.persistentDataPath, "Storage");

    static Storage()
    {
        if (!Directory.Exists(StorageDirectory))
        {
            Directory.CreateDirectory(StorageDirectory);
        }
    }

    public static T Load<T>(string fileName = null) where T : new()
    {
        string filePath = GetFullName<T>(fileName);
        if (!File.Exists(filePath))
        {
            return new T();
        }
        var data = File.ReadAllText(filePath);
        return JsonUtility.FromJson<T>(data);
    }

    public static void Save<T>(T Value, string fileName = null)
    {
        string filePath = GetFullName<T>(fileName);
        File.WriteAllText(filePath, JsonUtility.ToJson(Value));
    }

    private static string GetFullName<T>(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            fileName = typeof(T).Name + ".json";
        }
        return Path.Combine(StorageDirectory, fileName);
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Storage/Clear")]
    public static void ClearStorage()
    {
        Directory.Delete(StorageDirectory, true);
        Directory.CreateDirectory(StorageDirectory);
    }
#endif
}
