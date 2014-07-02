using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MiniGameData
{
    public GameObject ObjectToPlayWith;
    public Vector3 objectPosition;
    //public Quaternion objectRotation;
    public Vector2[] requiredResultRanges;
    public Texture backgroundTexture;
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
        return (float)System.Math.Round(baseHealth, 1) * healthLevels [healthCurrentLevel].multiplicator;
    }
    public string GetAdditionalHealthString()
    {
        if (healthCurrentLevel + 1 >= healthLevels.Length)
        {
            return "-1";
        }

        string result_string = "";
        float result_float = (float)System.Math.Round(baseHealth, 1) * (healthLevels [healthCurrentLevel + 1].multiplicator - healthLevels [healthCurrentLevel].multiplicator);
        result_float = (float)System.Math.Round(result_float, 1);
                          
        if (result_float >= 0)
        {
            result_string = "+";
        }

        result_string += result_float.ToString();
         
        return result_string;
    }
    public float GetAdditionalHealthFloat()
    {
        float result;

        if (healthCurrentLevel + 1 >= healthLevels.Length)
        {
            result = -1f;
        }
        else
        {
            result = (float)System.Math.Round(baseHealth, 1) * (healthLevels [healthCurrentLevel + 1].multiplicator - healthLevels [healthCurrentLevel].multiplicator);
        }
        
        return (float)System.Math.Round(result, 1);
    }
    public float GetShootCooldown()
    {
        return (float)System.Math.Round(baseShootCooldown, 1) * shootCooldownLevels [shootCooldownCurrentLevel].multiplicator;
    }
    public string GetAdditionalShootCooldownString()
    {
        if (shootCooldownCurrentLevel + 1 >= shootCooldownLevels.Length)
        {
            return "-1";
        }

        string result_string = "";
        float result_float = (float)System.Math.Round(baseShootCooldown, 1) * (shootCooldownLevels [shootCooldownCurrentLevel + 1].multiplicator - shootCooldownLevels [shootCooldownCurrentLevel].multiplicator);
        result_float = (float)System.Math.Round(result_float, 1);

        if (result_float >= 0)
        {
            result_string = "+";
        }
        
        result_string += result_float.ToString();
        
        return result_string;        
    }
    public float GetAdditionalShootCooldownFloat()
    {
        float result;

        if (shootCooldownCurrentLevel + 1 >= shootCooldownLevels.Length)
        {
            result = -1f;
        }
        else
        {
            result = (float)System.Math.Round(baseShootCooldown, 1) * (shootCooldownLevels [shootCooldownCurrentLevel + 1].multiplicator - shootCooldownLevels [shootCooldownCurrentLevel].multiplicator);
        }

        return (float)System.Math.Round(result, 1);
    }
    public float GetDamage()
    {
        return (float)System.Math.Round(baseDamage, 1) * damageLevels [damageCurrentLevel].multiplicator;
    }
    public string GetAdditionalDamageString()
    {
        if (damageCurrentLevel + 1 >= damageLevels.Length)
        {
            return "-1";
        }

        string result_string = "";
        float result_float = (float)System.Math.Round(baseDamage, 1) * (damageLevels [damageCurrentLevel + 1].multiplicator - damageLevels [damageCurrentLevel].multiplicator);
        result_float = (float)System.Math.Round(result_float, 1);

        if (result_float >= 0)
        {
            result_string = "+";
        }
        
        result_string += result_float.ToString();
        
        return result_string;          
    }
    public float GetAdditionalDamageFloat()
    {
        float result;

        if (damageCurrentLevel + 1 >= damageLevels.Length)
        {
            result = -1f;
        }
        else
        {
            result = (float)System.Math.Round(baseDamage, 1) * (damageLevels [damageCurrentLevel + 1].multiplicator - damageLevels [damageCurrentLevel].multiplicator);
        }

        return (float)System.Math.Round(result, 1);
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
public class EnemyData
{
    public string name;
    public GameObject prefab;
    public int health;
    public float movementSpeed;
    public int worth;
    public int damage;
    public float damageCooldown;
}

[System.Serializable]
public class WaveData
{
    public float initialCooldown;
    public float newWaveCooldown;
    //public float spawnCooldown;
    public int numberOfEnemies;
    public EnemyData[] enemies;
    public float multiplicator;
    public int enemyNumIncrease;
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
    public Texture groundTexture;
    public Color cameraBackground;
    public WaveData wavesData;
}

public class GameDataController : MonoBehaviour 
{
    public TowerData[] towersData;
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
            }
            else
            {
                towersData[i].upgradeData.isUnlocked = false;
            }
            
            towersData[i].mainGameData.stats.healthCurrentLevel = PlayerPrefs.GetInt("HealthLevel_" + i.ToString(), 0);
            towersData[i].mainGameData.stats.damageCurrentLevel = PlayerPrefs.GetInt("DamageLevel_" + i.ToString(), 0);
            towersData[i].mainGameData.stats.shootCooldownCurrentLevel = PlayerPrefs.GetInt("CooldownLevel_" + i.ToString(), 0);
        }
    }
}
