using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Save : MonoBehaviour
{
    public void SaveScore(int s)
    {
        int highscore = GetScores()[1];

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/scores.dat");
        SaveData data = new SaveData {Score = s, Highscore = s >= highscore ? s : highscore};

        bf.Serialize(file, data);
        file.Close();
    }

    public int[] GetScores()
    {
        if (File.Exists(Application.persistentDataPath + "/scores.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/scores.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            return new[] {data.Score, data.Highscore};
        }

        Debug.LogError("No save file");
        return new[] {0, 0};
    }
}

[Serializable]
class SaveData
{
    public int Highscore;
    public int Score;
}
