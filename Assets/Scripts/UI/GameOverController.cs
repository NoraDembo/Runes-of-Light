using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameObject unlockDisplayPrefab;
    public GameObject specificKillPrefab;


    CanvasGroup displayWindow;
    Text time;
    Text kills;
    Text multiHit;
    GameObject specificKillsList;
    GameObject unlockList;

    bool fadeIn = false;
    public float fadeInRate = 0.5f;

    void Start()
    {
        displayWindow = GetComponent<CanvasGroup>();
        displayWindow.alpha = 0;
        time = transform.Find("GameOverTime").GetComponent<Text>();
        kills = transform.Find("GameOverKills").GetComponent<Text>();
        multiHit = transform.Find("GameOverMultiHit").GetComponent<Text>();
        specificKillsList = transform.Find("GameOverKillsList").gameObject;
        unlockList = transform.Find("GameOverUnlocksList").gameObject;
    }

    void Update()
    {
        if (fadeIn)
        {
            displayWindow.alpha += fadeInRate * Time.deltaTime;
        }
    }

    public void TriggerScreen()
    {
        fadeIn = true;

        transform.Find("PlayAgain").GetComponent<Button>().interactable = true;
        transform.Find("Quit").GetComponent<Button>().interactable = true;

        float timePassed = Time.timeSinceLevelLoad;
        int minutes = (int)(timePassed / 60);
        int seconds = (int)(timePassed % 60);
        time.text = (minutes < 10 ? "0" + minutes : minutes.ToString()) + ":" + (seconds < 10 ? "0" + seconds : seconds.ToString());
        multiHit.text = "Biggest Multi-Hit:  " + GameManager.sessionLargestMultiHit;
        kills.text = "Kills:  " + GameManager.sessionKills;

        for (int i = 0; i < GameManager.enemyTypes.Length; i++)
        {
            GameObject entry = Instantiate(specificKillPrefab);
            entry.transform.SetParent(specificKillsList.transform, false);
            entry.GetComponent<Text>().text = " - " + GameManager.enemyTypes[i] + ":  " + GameManager.sessionMonsterKills[i];
        }

        foreach (Ability a in GameManager.unlockedAbilities)
        {
            GameObject entry = Instantiate(unlockDisplayPrefab);
            entry.transform.SetParent(unlockList.transform, false);

            RuneDisplayElement rde = entry.GetComponent<RuneDisplayElement>();
            rde.SetValues(a);
        }
    }

    public void Quit()
    {
        GameManager.screenFader.BackToMenu();
    }

    public void Again()
    {
        GameManager.screenFader.StartGame();
    }
}

