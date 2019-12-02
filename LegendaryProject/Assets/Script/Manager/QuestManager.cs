using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    public enum Targets
    {
        TigreSpectral,MrBaleine
    }
    private int NumberOfEnemiesKilled=0;
    public int NumberOfEnemiesToKill=5;
    private bool progressEnabled;
    public Targets TargetName;
    private Texture textureQuest;
    private Rect rect;
    private Rect textRect;
    public GameObject pnj;
    public Transform transformPnj;
    public void UpdateQuesting (string deadName) {
        if (deadName == TargetName.ToString())
        {
            NumberOfEnemiesKilled++;
            if (NumberOfEnemiesToKill <= NumberOfEnemiesKilled && !progressEnabled)
            {
                Instantiate(pnj, transformPnj);
                progressEnabled = true;
                EnableStoryProgress();
            }
        }
	}


    public void EnableStoryProgress()
    {
        SpeechEnabler[] dialogs = FindObjectsOfType<SpeechEnabler>();
        foreach (SpeechEnabler dialog in dialogs)
        {
            dialog.UpdateDialogs();
        }
    }

    private void OnGUI()
    {
            rect = new Rect(10, 10, 500, 30);
            textRect = new Rect(10, 10, 100, 80);
            GUIStyle style = new GUIStyle();
            style.fontSize = 26;
            style.normal.textColor = Color.white;
            GUI.Box(rect, textureQuest);
            GUI.Label(textRect, progressEnabled?"Quête terminée!":TargetName + " à tuer : " + NumberOfEnemiesKilled + " / " + NumberOfEnemiesToKill, style);
    }
}
