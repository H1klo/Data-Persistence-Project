using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class DataFlow : MonoBehaviour
{
    // Start is called before the first frame update
    public static DataFlow dataFlow;
    [SerializeField]public string leaderName;
    [SerializeField] public string currentName;
    [SerializeField] public string bestScore;
    [SerializeField] public string previousName;
    public InputField textField;
    public Text textScore;


    
    private void Awake()
    {
        if(dataFlow!= null)
        {
            Destroy(gameObject);
            return;
        }
        dataFlow = this;
        DontDestroyOnLoad(gameObject);
        
        
        
    }
    private void Start()
    {
        LoadData();
        textScore.text = "Best score: " + leaderName + ": " + bestScore;
        textField.text = previousName;
    }

    public void StartPressed()
    {
        currentName = textField.text;
        
        SceneManager.LoadScene(0);
    }


    public void ExitPressed()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        SaveData("", "","");
    }
    // Update is called once per frame
    [System.Serializable]
    class DataToSave
    {
        public string leaderName;
        public string bestScore;
        public string previousName;
    }

    
    public void SaveData(string newUser, string newScore, string currentName)
    {
        
        DataToSave dataToSave = new DataToSave();
        dataToSave.bestScore = newScore;
        dataToSave.leaderName = newUser;
        dataToSave.previousName = currentName;
        Debug.Log(dataToSave.previousName);
        string json = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("saved");
    }
    
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            DataToSave dataToSave = JsonUtility.FromJson<DataToSave>(json);
            bestScore = dataToSave.bestScore;
            leaderName = dataToSave.leaderName;
            previousName = dataToSave.previousName;
            try { textField.text = dataToSave.previousName; }
            catch { }
            Debug.Log(previousName);
        }
    }

    
    
}
