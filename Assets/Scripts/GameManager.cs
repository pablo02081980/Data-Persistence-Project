using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string namePlayer;
    public int BestScore;
    public string nameBestScorePlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public int m_BestScore;
        public string namePlayer;
        public string nameBestScorePlayer;
    }

    public void SaveNameAndScore(string nameToSave, int numberBestScore, string nameBestScorePlayer)
    {
        SaveData data = new SaveData();
        data.m_BestScore = numberBestScore;
        data.namePlayer = nameToSave;
        data.nameBestScorePlayer = nameBestScorePlayer;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        //Debug.Log(Application.persistentDataPath);
    }
    public void LoadNameAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            BestScore = data.m_BestScore;
            //namePlayer = data.namePlayer;
            nameBestScorePlayer = data.nameBestScorePlayer;
        }
    }
}
