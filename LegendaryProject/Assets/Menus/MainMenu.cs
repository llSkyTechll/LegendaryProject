using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private GameObject player;

	public void NewGame()
    {
        Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    virtual public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "gameInfo.data", FileMode.Create);
        PlayerData playerDataToSave = new PlayerData();
        playerDataToSave.playerPositionX = (float)156.5;
        playerDataToSave.playerPositionY = (float)5.48;
        playerDataToSave.playerPositionZ = (float)124.5;
        playerDataToSave.zone = "Forest";
        bf.Serialize(file, playerDataToSave);
        file.Close();
    }

    public void LoadGame()
    {        
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "gameInfo.data"))
        {
            NewGame();
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + "gameInfo.data", FileMode.Open);
            PlayerData playerDataToLoad = (PlayerData)bf.Deserialize(file);
            SceneManager.LoadScene(playerDataToLoad.zone);
            file.Close();
        }
    }

    virtual public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
