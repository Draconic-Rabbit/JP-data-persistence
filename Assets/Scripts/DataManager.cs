using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    private string _playerName = "Player";

    public static DataManager Instance { get => _instance; set => _instance = value; }
    public string PlayerName { get => _playerName; set => _playerName = value; }

    public int bestScore = 0;
    public string bestScorePlayerName = "Nobody";

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int score;
    }

    public void SaveBestScore(int score)
    {
        bestScore = score;
        bestScorePlayerName = _playerName;
        SaveScore();
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.name = bestScorePlayerName;
        data.score = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.score;
            bestScorePlayerName = data.name;
        }
    }
}
