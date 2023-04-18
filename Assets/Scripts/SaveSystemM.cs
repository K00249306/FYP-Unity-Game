using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystemM
{
    // Converts data to a binary file and saves data 
    public static void SaveStats(BattleSystemMultiplayer multiPlayerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/MultiPlayerData.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        MultiPlayerData data = new MultiPlayerData(multiPlayerData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Converts binary file back to a readable file and loads data  
    public static MultiPlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/MultiPlayerData.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MultiPlayerData data = formatter.Deserialize(stream) as MultiPlayerData;
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
