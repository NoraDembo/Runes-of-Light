using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemiesInput;
    public static GameObject[] enemies;

    public static string[] enemyTypes;

    public static float longestSurvivalTime;

    public static int totalKills;
    public static int sessionKills = 0;

    public static int[] totalMonsterKills;
    public static int[] sessionMonsterKills;

    public static int largestMultiHit;
    public static int sessionLargestMultiHit = 0;


    public static Ability[] selectedAbilities;
    public static List<Ability> unlockedAbilities = new List<Ability>();

    public static bool ingame = true;

    public static ScreenFader screenFader;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        enemies = enemiesInput;

        selectedAbilities = new Ability[5];

        screenFader = GameObject.Find("ScreenFader").GetComponent<ScreenFader>();
    }

    void Start()
    {
        enemyTypes = new string[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            enemyTypes[i] = enemies[i].GetComponent<EnemyController>().enemyType;
        }

        totalMonsterKills = new int[enemies.Length];
        sessionMonsterKills = new int[enemies.Length];

        for(int i = 0; i < selectedAbilities.Length; i++)
        {
            selectedAbilities[i] = AbilityManager.emptyAbility;
        }

        LoadPlayerPrefs();
        

        SceneManager.LoadScene(1);

    }


    public static void ReportMultiHit(int i)
    {
        if(i > sessionLargestMultiHit)
        {
            sessionLargestMultiHit = i;
        }
    }

    public static void ReportKill(EnemyController deadEnemy)
    {
        sessionKills++;

        for(int i = 0; i < enemies.Length; i++)
        {
            if (deadEnemy.enemyType == enemyTypes[i])
            {
                sessionMonsterKills[i]++;
            }
        }

    }

    public static void MainMenu()
    {
        SceneManager.LoadScene(1);
    }


    public static void StartRound()
    {
        WritePlayerPrefs();

        for (int i = 0; i < enemies.Length; i++)
        {
            sessionMonsterKills[i] = 0;
        }

        sessionKills = 0;
        sessionLargestMultiHit = 0;
        unlockedAbilities = new List<Ability>();

        ingame = true;

        SceneManager.LoadScene(2);
    }

    public static void GameOver()
    {
        ingame = false;

        if(Time.timeSinceLevelLoad > longestSurvivalTime)
        {
            longestSurvivalTime = Time.timeSinceLevelLoad;
        }

        totalKills += sessionKills;

        for (int i = 0; i < enemies.Length; i++)
        {
            totalMonsterKills[i] += sessionMonsterKills[i];
        }

        if (sessionLargestMultiHit > largestMultiHit)
        {
            largestMultiHit = sessionLargestMultiHit;
        }

        GameObject.Find("GameOverScreen").GetComponent<GameOverController>().TriggerScreen();

        WritePlayerPrefs();
    }

    public static void WritePlayerPrefs()
    {
        PlayerPrefs.SetFloat("LongestSurvival", longestSurvivalTime);
        PlayerPrefs.SetInt("TotalKills", totalKills);

        for(int i = 0; i < enemies.Length; i++)
        {
            PlayerPrefs.SetInt("Monster" + i + "Kills", totalMonsterKills[i]);
        }

        PlayerPrefs.SetInt("MultiHit", largestMultiHit);

        for(int i = 0; i < AbilityManager.allAbilities.Length; i++)
        {
            PlayerPrefs.SetInt("Ability" + i + "Unlocked", AbilityManager.allAbilities[i].IsUnlocked ? 1 : 0);
        }

        for (int i = 0; i < selectedAbilities.Length; i++)
        {
            PlayerPrefs.SetString("SelectedAbility" + i, selectedAbilities[i].AbilityName);
        }

    }

    public static void LoadPlayerPrefs()
    {
        longestSurvivalTime = PlayerPrefs.GetFloat("LongestSurvival");
        totalKills = PlayerPrefs.GetInt("TotalKills");

        for (int i = 0; i < enemies.Length; i++)
        {
            totalMonsterKills[i] = PlayerPrefs.GetInt("Monster" + i + "Kills");
        }

        largestMultiHit = PlayerPrefs.GetInt("MultiHit");

        for (int i = 0; i < AbilityManager.allAbilities.Length; i++)
        {
            AbilityManager.allAbilities[i].IsUnlocked = PlayerPrefs.GetInt("Ability" + i + "Unlocked") == 1;
        }

        for (int i = 0; i < selectedAbilities.Length; i++)
        {
            selectedAbilities[i] = AbilityManager.GetAbilityByName(PlayerPrefs.GetString("SelectedAbility" + i));
        }
    }

    public static void ResetGame()
    {
        PlayerPrefs.DeleteAll();

        longestSurvivalTime = 0;
        totalKills = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            totalMonsterKills[i] = 0;
        }

        largestMultiHit = 0;

        for (int i = 0; i < AbilityManager.allAbilities.Length; i++)
        {
            AbilityManager.allAbilities[i].IsUnlocked = false;
        }

        for (int i = 0; i < selectedAbilities.Length; i++)
        {
            selectedAbilities[i] = AbilityManager.emptyAbility;
        }

        WritePlayerPrefs();
    }

    public static int GetKillCountByName(string monsterName)
    {
        for(int i = 0; i < enemyTypes.Length; i++)
        {
            if(enemyTypes[i] == monsterName)
            {
                return sessionMonsterKills[i];
            }
        }

        return 0; //fallback

    }
}
