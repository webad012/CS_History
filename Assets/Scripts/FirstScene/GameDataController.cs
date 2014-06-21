﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class MiniGameData
{
    public GameObject ObjectToPlayWith;
    public Vector2[] requiredResultRanges;
    public Material backgroundMaterial;
}

[System.Serializable]
public class UpgradableStat
{
    public int price;
    public float multiplicator;
}

[System.Serializable]
public class TowerDefenseStats
{
    public float baseHealth;
    public int healthCurrentLevel;
    public UpgradableStat[] healthLevels;

    public float baseShootCooldown;
    public int shootCooldownCurrentLevel;
    public UpgradableStat[] shootCooldownLevels;

    public float baseDamage;
    public int damageCurrentLevel;
    public UpgradableStat[] damageLevels;

    public float GetHealth()
    {
        return baseHealth * healthLevels [healthCurrentLevel].multiplicator;
    }
    public float GetShootCooldown()
    {
        return baseShootCooldown * shootCooldownLevels [shootCooldownCurrentLevel].multiplicator;
    }
    public float GetDamage()
    {
        return baseDamage * damageLevels [damageCurrentLevel].multiplicator;
    }
}

[System.Serializable]
public class MainGameData
{
    public GameObject towerPrefab;
    public GameObject projectilePrefab;
    public int price;
    public string spritename;
    public TowerDefenseStats stats;
}

[System.Serializable]
public class UpgradeGameData
{
    public GameObject towerUnlockedPrefab;
    public GameObject towerLockedPrefab;
    public bool isUnlocked = false;
}

[System.Serializable]
public class TowerData
{
    public string towerName;
    public MiniGameData miniGameData;
    public MainGameData mainGameData;
    public UpgradeGameData upgradeData;
}

[System.Serializable]
public class House
{
    public int coinsGain;
    public int coinsTimeout;
    public int knowledgeGain;
    public int knowledgeTimeout;
}

[System.Serializable]
public class Level
{
    public string levelName;
    public string location;
    public string time;
    public string spritename;
    public int knowledgeRequired;
    public int startingCoins;
    public House house;
}

public class GameDataController : MonoBehaviour 
{
    public TowerData[] towersData;

    public GameObject levelPrefab;
    public Level[] levels;
    
    public bool canContinue = false;

    void Awake()
    {
        DontDestroyOnLoad (this);
    }

	// Use this for initialization
	void Start () 
    {
        StartCoroutine(Initializator());
	}

    IEnumerator Initializator()
    {
        InitializeTowers();

        canContinue = true;

        yield return null;
    }

    public void InitializeTowers()
    {
        for (int i=0; i<towersData.Length; i++)
        {
            if(PlayerPrefs.GetInt("TowerUnlocked" + i.ToString(), 0) != 0)
            {
                towersData[i].upgradeData.isUnlocked = true;
                
                towersData[i].mainGameData.stats.healthCurrentLevel = PlayerPrefs.GetInt("HealthLevel_" + i.ToString(), 0);
                towersData[i].mainGameData.stats.damageCurrentLevel = PlayerPrefs.GetInt("DamageLevel_" + i.ToString(), 0);
                towersData[i].mainGameData.stats.shootCooldownCurrentLevel = PlayerPrefs.GetInt("CooldownLevel_" + i.ToString(), 0);
            }
            else
            {
                towersData[i].upgradeData.isUnlocked = false;
            }
        }
    }
}
