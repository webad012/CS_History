using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class UpgradeControllerStats
{
    public UILabel damageLabel;
    public UILabel cooldownLabel;
    public UILabel healthLabel;

    public GameObject damageButton;
    public GameObject cooldownButton;
    public GameObject healthButton;

    public UILabel damageAdditionLabel;
    public UILabel cooldownAdditionLabel;
    public UILabel healthAdditionLabel;
}

public class UpgradeController : MonoBehaviour 
{
    public float optionTransitionSpeed = 0.25f;
    public float outLeftX;
    public float outRightX;
    public UILabel towerNameLabel;
    public UILabel knowledgeValueLabel;

    public UpgradeControllerStats upgradeStats;

    private int currentTower;
    private Vector3 outLeftVector;
    private Vector3 outRightVector;

    private List<GameObject> towerObjects;

    private GameDataController gameDataControllerScript;

	// Use this for initialization
	void Start () 
    {
        upgradeStats.healthAdditionLabel.text = "";
        upgradeStats.damageAdditionLabel.text = "";
        upgradeStats.cooldownAdditionLabel.text = "";

        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        outLeftVector = new Vector3(outLeftX, 0, 0);
        outRightVector = new Vector3(outRightX, 0, 0);

        towerObjects = new List<GameObject>();
        for (int i=0; i<gameDataControllerScript.towersData.Length; i++)
        {
            GameObject childTower;

            if(gameDataControllerScript.towersData[i].upgradeData.isUnlocked)
            {
                childTower = (GameObject)Instantiate(gameDataControllerScript.towersData[i].upgradeData.towerUnlockedPrefab, outRightVector, Quaternion.identity);
            }
            else
            {
                childTower = (GameObject)Instantiate(gameDataControllerScript.towersData[i].upgradeData.towerLockedPrefab, outRightVector, Quaternion.identity);
            }

            childTower.name = gameDataControllerScript.towersData[i].towerName;

            towerObjects.Add(childTower);
        }

        towerObjects [currentTower].transform.localPosition = Vector3.zero;

        UpdateGUI();
	}

    void UpdateGUI()
    {
        string knowledge_string;
        int knowledge_int = PlayerPrefs.GetInt("PlayerKnowledge", 0);
        if (knowledge_int == 0)
        {
            knowledge_string = knowledge_int.ToString();
        }
        else
        {
            knowledge_string = knowledge_int.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        }

        knowledgeValueLabel.text = knowledge_string;

        upgradeStats.damageLabel.text = "Tower damage: ";
        upgradeStats.cooldownLabel.text = "Tower cooldown: ";
        upgradeStats.healthLabel.text = "Tower health: ";

        if (gameDataControllerScript.towersData [currentTower].upgradeData.isUnlocked)
        {
            towerNameLabel.text = gameDataControllerScript.towersData [currentTower].towerName;
            upgradeStats.damageLabel.text += gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetDamage().ToString();
            upgradeStats.cooldownLabel.text += gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetShootCooldown().ToString();
            upgradeStats.healthLabel.text += gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetHealth().ToString();
        

            if(gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageCurrentLevel+1 >= gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageLevels.Length
               || knowledge_int < gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageLevels[gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageCurrentLevel+1].price)
            {
                if(gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageCurrentLevel+1 >= gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageLevels.Length)
                {
                    upgradeStats.damageButton.transform.Find("Label").GetComponent<UILabel>().text = "MAX";
                }
                upgradeStats.damageButton.GetComponent<UIButton>().isEnabled = false;
            }
            else
            {
                upgradeStats.damageButton.GetComponent<UIButton>().isEnabled = true;
            }

            if(gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownCurrentLevel+1 >= gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownLevels.Length
               || knowledge_int < gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownLevels[gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownCurrentLevel+1].price)
            {
                if(gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownCurrentLevel+1 >= gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownLevels.Length)
                {
                    upgradeStats.cooldownButton.transform.Find("Label").GetComponent<UILabel>().text = "MAX";
                }
                upgradeStats.cooldownButton.GetComponent<UIButton>().isEnabled = false;
            }
            else
            {
                upgradeStats.cooldownButton.GetComponent<UIButton>().isEnabled = true;
            }

            if(gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthCurrentLevel+1 >= gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthLevels.Length
               || knowledge_int < gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthLevels[gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthCurrentLevel+1].price)
            {
                if(gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthCurrentLevel+1 >= gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthLevels.Length)
                {
                    upgradeStats.cooldownButton.transform.Find("Label").GetComponent<UILabel>().text = "MAX";
                }
                upgradeStats.healthButton.GetComponent<UIButton>().isEnabled = false;
            }
            else
            {
                upgradeStats.healthButton.GetComponent<UIButton>().isEnabled = true;
            }
        } 
        else
        {
            towerNameLabel.text = "Locked";

            upgradeStats.damageLabel.text += "NaN";
            upgradeStats.damageButton.collider.enabled = false;

            upgradeStats.cooldownLabel.text += "NaN";
            upgradeStats.cooldownButton.collider.enabled = false;

            upgradeStats.healthLabel.text += "NaN";
            upgradeStats.healthButton.collider.enabled = false;
        }
    }

    void ButtonLeft()
    {
        if (currentTower > 0)
        {
            TweenPosition.Begin(towerObjects[currentTower], optionTransitionSpeed, outRightVector);
            currentTower--;
            TweenPosition.Begin(towerObjects[currentTower], optionTransitionSpeed, new Vector3(0, 0, 0));
            UpdateGUI();
        }
    }
    
    void ButtonRight()
    {
        if (currentTower != towerObjects.Count - 1)
        {
            TweenPosition.Begin(towerObjects[currentTower], optionTransitionSpeed, outLeftVector);
            currentTower++;
            TweenPosition.Begin(towerObjects[currentTower], optionTransitionSpeed, new Vector3(0, 0, 0));
            UpdateGUI();
        }
    }

    void ButtonBack()
    {
        Application.LoadLevel("MainMenu");
    }

    void OnHoverDamage()
    {
        string result = "Error";

        int currentUpgradeLevel = gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageCurrentLevel;
        int numberOfLevels = gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageLevels.Length;
        
        if(currentUpgradeLevel+1 >= numberOfLevels)
        {
            result = "MAX";
        }
        else
        {
            float upgradePrice = gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageLevels[currentUpgradeLevel+1].price;
            result = "Price: " + upgradePrice.ToString();

            float additionalDamage = gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetAdditionalDamage();
            upgradeStats.damageAdditionLabel.text = "";
            if(additionalDamage > 0)
            {
                upgradeStats.damageAdditionLabel.text += "+";
            }
            upgradeStats.damageAdditionLabel.text += additionalDamage.ToString();
        }

        upgradeStats.damageButton.transform.Find("Label").GetComponent<UILabel>().text = result;
    }

    void OffHoverDamage()
    {
        upgradeStats.damageButton.transform.Find("Label").GetComponent<UILabel>().text = "Upgrade";
        upgradeStats.damageAdditionLabel.text = "";
    }

    void ButtonDamage()
    {
        TowerDefenseStats stats = gameDataControllerScript.towersData [currentTower].mainGameData.stats;
        
        int nextUpgradeLevel = stats.damageCurrentLevel+1;
        int numberOfLevels = stats.damageLevels.Length;
        int upgradePrice = stats.damageLevels[nextUpgradeLevel].price;

        int knowledge_int = PlayerPrefs.GetInt("PlayerKnowledge", 0);
        if (nextUpgradeLevel < numberOfLevels
            && knowledge_int >= upgradePrice)
        {
            knowledge_int -= upgradePrice;
            PlayerPrefs.SetInt("PlayerKnowledge", knowledge_int);
            
            PlayerPrefs.SetInt("DamageLevel_" + currentTower.ToString(), nextUpgradeLevel);
            
            gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageCurrentLevel = nextUpgradeLevel;

            UpdateGUI();

        }
    }

    void OnHoverHealth()
    {
        string result = "Error";
        
        int nextUpgradeLevel = gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthCurrentLevel+1;
        int numberOfLevels = gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthLevels.Length;
        
        if(nextUpgradeLevel >= numberOfLevels)
        {
            result = "MAX";
        }
        else
        {
            float upgradePrice = gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthLevels[nextUpgradeLevel].price;
            result = "Price: " + upgradePrice.ToString();

            float additionalHealth = gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetAdditionalHealth();
            upgradeStats.healthAdditionLabel.text = "";
            if(additionalHealth > 0)
            {
                upgradeStats.healthAdditionLabel.text += "+";
            }
            upgradeStats.healthAdditionLabel.text += additionalHealth.ToString();
            
            //upgradeStats.healthAdditionLabel.text = "+" + gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetAdditionalHealth().ToString();
        }
        
        upgradeStats.healthButton.transform.Find("Label").GetComponent<UILabel>().text = result;
    }

    void OffHoverHealth()
    {
        upgradeStats.healthButton.transform.Find("Label").GetComponent<UILabel>().text = "Upgrade";
        upgradeStats.healthAdditionLabel.text = "";
    }

    void ButtonHealth()
    {
        TowerDefenseStats stats = gameDataControllerScript.towersData [currentTower].mainGameData.stats;
        
        int nextUpgradeLevel = stats.healthCurrentLevel+1;
        int numberOfLevels = stats.healthLevels.Length;
        int upgradePrice = stats.healthLevels[nextUpgradeLevel].price;
        
        int knowledge_int = PlayerPrefs.GetInt("PlayerKnowledge", 0);
        if (nextUpgradeLevel < numberOfLevels
            && knowledge_int >= upgradePrice)
        {
            knowledge_int -= upgradePrice;
            PlayerPrefs.SetInt("PlayerKnowledge", knowledge_int);
            
            PlayerPrefs.SetInt("HealthLevel_" + currentTower.ToString(), nextUpgradeLevel);
            
            gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthCurrentLevel = nextUpgradeLevel;
            
            UpdateGUI();
        }
    }

    void OnHoverCooldown()
    {
        string result = "Error";

        int nextUpgradeLevel = gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownCurrentLevel + 1;
        int numberOfLevels = gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownLevels.Length;
        
        if(nextUpgradeLevel >= numberOfLevels)
        {
            result = "MAX";
        }
        else
        {
            float upgradePrice = gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownLevels[nextUpgradeLevel].price;
            result = "Price: " + upgradePrice.ToString();

            float additionalCooldown = gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetAdditionalShootCooldown();
            upgradeStats.cooldownAdditionLabel.text = "";
            if(additionalCooldown > 0)
            {
                upgradeStats.cooldownAdditionLabel.text += "+";
            }
            upgradeStats.cooldownAdditionLabel.text += additionalCooldown.ToString();

            //upgradeStats.cooldownAdditionLabel.text = "+" + gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetAdditionalShootCooldown().ToString();
        }
        
        upgradeStats.cooldownButton.transform.Find("Label").GetComponent<UILabel>().text = result;
    }

    void OffHoverCooldown()
    {
        upgradeStats.cooldownButton.transform.Find("Label").GetComponent<UILabel>().text = "Upgrade";
        upgradeStats.cooldownAdditionLabel.text = "";
    }

    void ButtonCooldown()
    {
        TowerDefenseStats stats = gameDataControllerScript.towersData [currentTower].mainGameData.stats;
        
        int nextUpgradeLevel = stats.shootCooldownCurrentLevel+1;
        int numberOfLevels = stats.shootCooldownLevels.Length;
        int upgradePrice = stats.shootCooldownLevels[nextUpgradeLevel].price;
        
        int knowledge_int = PlayerPrefs.GetInt("PlayerKnowledge", 0);
        if (nextUpgradeLevel < numberOfLevels
            && knowledge_int >= upgradePrice)
        {
            knowledge_int -= upgradePrice;
            PlayerPrefs.SetInt("PlayerKnowledge", knowledge_int);
            
            PlayerPrefs.SetInt("CooldownLevel_" + currentTower.ToString(), nextUpgradeLevel);
            
            gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownCurrentLevel = nextUpgradeLevel;
            
            UpdateGUI();
            
        }
    }
}
