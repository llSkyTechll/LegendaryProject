using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MainMenu {

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject player;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public override void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "gameInfo.data", FileMode.Create);
        PlayerData playerDataToSave = new PlayerData();
        playerDataToSave.playerPositionX = PositionManager.instance.player.transform.position.x;
        playerDataToSave.playerPositionY = PositionManager.instance.player.transform.position.y;
        playerDataToSave.playerPositionZ = PositionManager.instance.player.transform.position.z;
        playerDataToSave.zone = SceneManager.GetActiveScene().name;
        bf.Serialize(file, playerDataToSave);
        file.Close();
        Resume();
    }

    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Menu");
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public override void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
        Debug.Log("Quitting game...");
    }
}
