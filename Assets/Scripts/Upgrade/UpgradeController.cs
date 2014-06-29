﻿using UnityEngine;
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
    public float optionTransitionSpeed = 0.5f;
    public UILabel towerNameLabel;
    public UILabel knowledgeValueLabel;

    public UpgradeControllerStats upgradeStats;

    private string towerName;
    private string damageInfoString;
    private string damageButtonString;
    private string damageAdditionString;
    private string cooldowInfoString;
    private string cooldowButtonString;
    private string cooldowAdditionString;
    private string healthInfoString;
    private string healthButtonString;
    private string healthAdditionString;
    private string knowledgeString;

    private int currentTower = 0;
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

        outLeftVector = new Vector3(-Screen.width, 0, 0);
        outRightVector = new Vector3(Screen.width, 0, 0);

        towerObjects = new List<GameObject>();
        for (int i=0; i<gameDataControllerScript.towersData.Length; i++)
        {
            Vector3 posVector = Vector3.zero;
            if(i < currentTower)
            {
                posVector = outLeftVector;
            }
            else if(i > currentTower)
            {
                posVector = outRightVector;
            }

            GameObject childTower;

            if(gameDataControllerScript.towersData[i].upgradeData.isUnlocked)
            {
                childTower = (GameObject)Instantiate(gameDataControllerScript.towersData[i].upgradeData.towerUnlockedPrefab, posVector, Quaternion.identity);
            }
            else
            {
                childTower = (GameObject)Instantiate(gameDataControllerScript.towersData[i].upgradeData.towerLockedPrefab, posVector, Quaternion.identity);
            }

            childTower.name = gameDataControllerScript.towersData[i].towerName;
            towerObjects.Add(childTower);
        }

        //UpdateGUI();
	}

    void Update()
    {
        int knowledge_int = PlayerPrefs.GetInt("PlayerKnowledge", 0);
        if (knowledge_int == 0)
        {
            knowledgeString = knowledge_int.ToString();
        }
        else
        {
            knowledgeString = knowledge_int.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
        }

        damageInfoString = "Tower damage: ";
        cooldowInfoString = "Tower cooldown: ";
        healthInfoString = "Tower health: ";

        if (gameDataControllerScript.towersData [currentTower].upgradeData.isUnlocked)
        {
            towerName = gameDataControllerScript.towersData [currentTower].towerName;

            TowerDefenseStats towerStats = gameDataControllerScript.towersData [currentTower].mainGameData.stats;

            damageInfoString += towerStats.GetDamage().ToString();
            cooldowInfoString += towerStats.GetShootCooldown().ToString();
            healthInfoString += towerStats.GetHealth().ToString();

            damageAdditionString = towerStats.GetAdditionalDamageString();
            cooldowAdditionString = towerStats.GetAdditionalShootCooldownString();
            healthAdditionString = towerStats.GetAdditionalHealthString();

            if(towerStats.damageCurrentLevel+1 >= towerStats.damageLevels.Length)
            {
                upgradeStats.damageButton.GetComponent<UIButton>().isEnabled = false;
                damageButtonString = "MAX";
            }
            else
            {
                if(knowledge_int < towerStats.damageLevels[towerStats.damageCurrentLevel+1].price)
                {
                    upgradeStats.damageButton.GetComponent<UIButton>().isEnabled = false;
                }
                else
                {
                    upgradeStats.damageButton.GetComponent<UIButton>().isEnabled = true;
                }
                damageButtonString = "Upgrade: " + towerStats.damageLevels[towerStats.damageCurrentLevel+1].price.ToString();
            }

            if(towerStats.shootCooldownCurrentLevel+1 >= towerStats.shootCooldownLevels.Length)
            {
                upgradeStats.cooldownButton.GetComponent<UIButton>().isEnabled = false;
                cooldowButtonString = "MAX";
            }
            else
            {
                if(knowledge_int < towerStats.shootCooldownLevels[towerStats.shootCooldownCurrentLevel+1].price)
                {
                    upgradeStats.cooldownButton.GetComponent<UIButton>().isEnabled = false;
                }
                else
                {
                    upgradeStats.cooldownButton.GetComponent<UIButton>().isEnabled = true;
                }
                cooldowButtonString = "Upgrade: " + towerStats.shootCooldownLevels[towerStats.shootCooldownCurrentLevel+1].price.ToString();
            }

            if(towerStats.healthCurrentLevel+1 >= towerStats.healthLevels.Length)
            {
                upgradeStats.healthButton.GetComponent<UIButton>().isEnabled = false;
                healthButtonString = "MAX";
            }
            else
            {
                if(knowledge_int < towerStats.healthLevels[towerStats.healthCurrentLevel+1].price)
                {
                    upgradeStats.healthButton.GetComponent<UIButton>().isEnabled = false;
                }
                else
                {
                    upgradeStats.healthButton.GetComponent<UIButton>().isEnabled = true;
                }
                healthButtonString = "Upgrade: " + towerStats.healthLevels[towerStats.healthCurrentLevel+1].price.ToString();
            }
        }
        else
        {
            towerName = "Locked";
            
            damageInfoString += "NaN";
            cooldowInfoString += "NaN";
            healthInfoString += "NaN";

            damageButtonString = "Locked";
            cooldowButtonString = "Locked";
            healthButtonString = "Locked";
            
            damageAdditionString = "";
            cooldowAdditionString = "";
            healthAdditionString = "";

            upgradeStats.damageButton.GetComponent<UIButton>().isEnabled = false;
            upgradeStats.cooldownButton.GetComponent<UIButton>().isEnabled = false;
            upgradeStats.healthButton.GetComponent<UIButton>().isEnabled = false;
        }

        UpdateGUI();
    }

    void UpdateGUI()
    {
        towerNameLabel.text = towerName;

        upgradeStats.damageLabel.text = damageInfoString;
        upgradeStats.cooldownLabel.text = cooldowInfoString;
        upgradeStats.healthLabel.text = healthInfoString;

        upgradeStats.damageAdditionLabel.text = damageAdditionString;
        upgradeStats.cooldownAdditionLabel.text = cooldowAdditionString;
        upgradeStats.healthAdditionLabel.text = healthAdditionString;

        upgradeStats.damageButton.transform.Find("Label").GetComponent<UILabel>().text = damageButtonString;
        upgradeStats.cooldownButton.transform.Find("Label").GetComponent<UILabel>().text = cooldowButtonString;
        upgradeStats.healthButton.transform.Find("Label").GetComponent<UILabel>().text = healthButtonString;

        knowledgeValueLabel.text = knowledgeString;

        /*if (gameDataControllerScript.towersData [currentTower].upgradeData.isUnlocked)
        {
            towerNameLabel.text = gameDataControllerScript.towersData [currentTower].towerName;
            upgradeStats.damageLabel.text += gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetDamage().ToString();
            upgradeStats.damageAdditionLabel.text = gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetAdditionalDamageString();

            upgradeStats.cooldownLabel.text += gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetShootCooldown().ToString();
            upgradeStats.cooldownAdditionLabel.text = gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetAdditionalShootCooldownString();

            upgradeStats.healthLabel.text += gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetHealth().ToString();
            upgradeStats.healthAdditionLabel.text = gameDataControllerScript.towersData [currentTower].mainGameData.stats.GetAdditionalHealthString();

            int currentUpgradeLevel = 0;
            float upgradePrice = 0f;
            upgradeStats.damageButton.transform.Find("Label").GetComponent<UILabel>().text = "Upgrade: ";
            currentUpgradeLevel = gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageCurrentLevel;
            upgradePrice = gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageLevels[currentUpgradeLevel+1].price;
            upgradeStats.damageButton.transform.Find("Label").GetComponent<UILabel>().text += upgradePrice.ToString();

            upgradeStats.cooldownButton.transform.Find("Label").GetComponent<UILabel>().text = "Upgrade: ";
            currentUpgradeLevel = gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownCurrentLevel;
            upgradePrice = gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownLevels[currentUpgradeLevel+1].price;
            upgradeStats.cooldownButton.transform.Find("Label").GetComponent<UILabel>().text += upgradePrice.ToString();

            upgradeStats.healthButton.transform.Find("Label").GetComponent<UILabel>().text = "Upgrade: ";
            currentUpgradeLevel = gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthCurrentLevel;
            upgradePrice = gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthLevels[currentUpgradeLevel+1].price;
            upgradeStats.healthButton.transform.Find("Label").GetComponent<UILabel>().text += upgradePrice.ToString();

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
                    upgradeStats.healthButton.transform.Find("Label").GetComponent<UILabel>().text = "MAX";
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
            upgradeStats.damageButton.transform.Find("Label").GetComponent<UILabel>().text = damageButtonString;
            upgradeStats.cooldownButton.transform.Find("Label").GetComponent<UILabel>().text = "Locked";
            upgradeStats.healthButton.transform.Find("Label").GetComponent<UILabel>().text = "Locked";

            upgradeStats.healthAdditionLabel.text = "";
            upgradeStats.cooldownAdditionLabel.text = "";
            upgradeStats.damageAdditionLabel.text = "";


            upgradeStats.damageLabel.text += "NaN";
            upgradeStats.damageButton.GetComponent<UIButton>().isEnabled = false;

            upgradeStats.cooldownLabel.text += "NaN";
            upgradeStats.cooldownButton.GetComponent<UIButton>().isEnabled = false;

            upgradeStats.healthLabel.text += "NaN";
            upgradeStats.healthButton.GetComponent<UIButton>().isEnabled = false;
        }*/
    }

    void ButtonLeft()
    {
        if (currentTower > 0)
        {
            TweenPosition.Begin(towerObjects[currentTower], optionTransitionSpeed, outRightVector);
            currentTower--;
            TweenPosition.Begin(towerObjects[currentTower], optionTransitionSpeed, Vector3.zero);
            UpdateGUI();
        }
    }
    
    void ButtonRight()
    {
        if (currentTower != towerObjects.Count - 1)
        {
            TweenPosition.Begin(towerObjects[currentTower], optionTransitionSpeed, outLeftVector);
            currentTower++;
            TweenPosition.Begin(towerObjects[currentTower], optionTransitionSpeed, Vector3.zero);
            UpdateGUI();
        }
    }

    void ButtonBack()
    {
        Application.LoadLevel("MainMenu");
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
