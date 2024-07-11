using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    public GameObject killListPrefab;

    Text timeValue;
    Text multikillValue;
    Text killsValue;

    Transform killsList;

    CanvasGroup warning;

    void Start()
    {
        warning = transform.Find("Warning").GetComponent<CanvasGroup>();

        timeValue = transform.Find("TimeValue").GetComponent<Text>();
        multikillValue = transform.Find("MultikillValue").GetComponent<Text>();
        killsValue = transform.Find("KillsValue").GetComponent<Text>();

        killsList = transform.Find("KillsList").transform;

        ShowStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowStats()
    {
        int minutes = (int)(GameManager.longestSurvivalTime / 60);
        int seconds = (int)(GameManager.longestSurvivalTime % 60);
        timeValue.text = (minutes < 10 ? "0" + minutes : minutes.ToString()) + ":" + (seconds < 10 ? "0" + seconds : seconds.ToString());
        multikillValue.text = GameManager.largestMultiHit.ToString();
        killsValue.text = GameManager.totalKills.ToString();

        for(int i = 0; i < killsList.childCount; i++)
        {
            Destroy(killsList.GetChild(i).gameObject);
        }

        for (int i = 0; i < GameManager.enemyTypes.Length; i++)
        {
            GameObject entry = Instantiate(killListPrefab);
            entry.transform.SetParent(killsList, false);
            entry.transform.Find("MonsterKillsLabel").GetComponent<Text>().text = GameManager.enemyTypes[i] + "s Killed :";
            entry.transform.Find("MonsterKillsValue").GetComponent<Text>().text = GameManager.totalMonsterKills[i].ToString();
        }
    }

    public void ResetProgress()
    {
        warning.alpha = 1;
        warning.transform.Find("Ok").GetComponent<Button>().interactable = true;
        warning.transform.Find("Cancel").GetComponent<Button>().interactable = true;
    }

    public void ConfirmReset(bool reset)
    {
        if (reset)
        {
            GameManager.ResetGame();
            ShowStats();
        }

        warning.transform.Find("Ok").GetComponent<Button>().interactable = false;
        warning.transform.Find("Cancel").GetComponent<Button>().interactable = false;
        warning.alpha = 0;
    }
}
