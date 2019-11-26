using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    public enum Targets
    {
        GhostTiger,WhaleBoss
    }
    private int NumberOfEnemiesKilled=0;
    public int NumberOfEnemiesToKill=5;
    public Targets TargetName;
    public Texture textureQuest;
    public Material material;
	public void UpdateQuesting (string deadName) {
        if (deadName == TargetName.ToString())
        {
            NumberOfEnemiesKilled++;
            if (NumberOfEnemiesToKill <= NumberOfEnemiesKilled)
            {

            }
        }
	}


    void EnableStoryProgress()
    {

    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 26;
        style.normal.textColor = Color.white;
        GUI.Box(new Rect(10, 10, 500, 30),textureQuest);
        GUI.Label(new Rect(10, 10, 100, 80), TargetName+" à tuer : " + NumberOfEnemiesKilled+" / "+ NumberOfEnemiesToKill, style);
        
    }
}
