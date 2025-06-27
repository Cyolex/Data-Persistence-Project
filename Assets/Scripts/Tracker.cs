using UnityEngine;
using System.IO;

public class Tracker : MonoBehaviour
{
    public static Tracker Instance;

    public string playerName;
    public int highScore;

    [System.Serializable]
    class SaveData
    {
        public int highScore;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(GetSavePath(), json);
    }

    public void LoadHighScore()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.highScore;
        }
    }

    private string GetSavePath()
    {
        return Application.persistentDataPath + "/highscore.json";
    }
}
