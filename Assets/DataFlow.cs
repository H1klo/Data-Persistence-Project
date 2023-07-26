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
    [SerializeField]public string userName;
    public InputField textField;
    private void Awake()
    {
        if(dataFlow!= null)
        {
            Destroy(gameObject);
            return;
        }
        dataFlow = this;
        DontDestroyOnLoad(gameObject);
        LoadName();
    }
    
    public void StartPressed()
    {
        SceneManager.LoadScene(0);
    }


    public void ExitPressed()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    // Update is called once per frame
    [System.Serializable]
    class SaveDataName
    {
        public string userName;
    }

    public void SaveName()
    {
        SaveDataName saveDataName = new SaveDataName();
        
        saveDataName.userName = textField.text;
        string json = JsonUtility.ToJson(saveDataName);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Name saved");
    }
    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataName saveDataName = JsonUtility.FromJson<SaveDataName>(json);
            userName = saveDataName.userName;
            try { textField.text = saveDataName.userName; }
            catch { };
            Debug.Log(userName);
        }

    }

    
    class SaveScoreData
    {

    }
}
