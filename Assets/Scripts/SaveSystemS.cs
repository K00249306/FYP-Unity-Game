using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystemS
{
    // Converts data to a binary file and saves data 
    public static void SaveStats(BattleSystem singlePlayerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SinglePlayerData.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SinglePlayerData data = new SinglePlayerData(singlePlayerData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Converts binary file back to a readable file and loads data  
    public static SinglePlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/SinglePlayerData.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SinglePlayerData data = formatter.Deserialize(stream) as SinglePlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No save file found in " + path);
            return null;
        }
    }
}
