using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController gameControl;


    public void Start()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "gameInfo.data"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "gameInfo.data", FileMode.Open);
            PlayerData playerDataToLoad = (PlayerData)bf.Deserialize(file);
            GameObject player = GameObject.FindWithTag("Player");
            if (playerDataToLoad.zone == SceneManager.GetActiveScene().name)
            {
                player.transform.position = new Vector3(playerDataToLoad.playerPositionX, playerDataToLoad.playerPositionY, playerDataToLoad.playerPositionZ);
            }
            file.Close();
        }
    }
}

[Serializable]
class PlayerData
{
    public string zone;
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;
}