using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpeechEnabler : MonoBehaviour {
    public enum Rooms
    {
       BossRoom,Labyrinth,Forest
    }
    private int textNumber;
    public Rooms RoomName;
    public Canvas SpeechBubble;
    public DialogText[] dialogs;
    public DialogText currentDialog;
    public Text canvasText;
	// Use this for initialization
	void Start () {
        SpeechBubble.enabled = false;
        textNumber = 0;
        ChangeText();
    }
	
	public void UpdateDialogs() {
        if (textNumber < dialogs.Length -1)
        {
           textNumber++;
           ChangeText();
        }
    }

    private void ChangeText()
    {
        DetermineCurrentDialog();
        string dialog = currentDialog.dialogText;
        canvasText.text = dialog;
    }

    private void DetermineCurrentDialog()
    {
        currentDialog = dialogs[textNumber];
    }

    public void BeginConversation()
    {
        SpeechBubble.enabled = true;
    }

    public void StopConversation()
    {
        SpeechBubble.enabled = false;
        print(textNumber);
        if (textNumber == dialogs.Length-1)
        {
            switch (RoomName)
            {
                case Rooms.BossRoom:
                    SceneManager.LoadScene("Boss Room");
                    break;
                case Rooms.Labyrinth:
                    SceneManager.LoadScene("Labyrinth");
                    break;
                case Rooms.Forest:
                    SceneManager.LoadScene("Forest");
                    break;
                default:
                    SceneManager.LoadScene("Forest");
                    break;
            }
           
        }
    }
}
