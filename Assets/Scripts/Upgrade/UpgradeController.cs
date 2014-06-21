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
    //private int knowledge_int;
    //private string knowledge_string;

    private List<GameObject> towerObjects;

    private GameDataController gameDataControllerScript;

	// Use this for initialization
	void Start () 
    {
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
        
            //Debug.Log(knowledge_int);
            //Debug.Log(gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageLevels[gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageCurrentLevel].price);
            if(knowledge_int < gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageLevels[gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageCurrentLevel+1].price)
            {
                //upgradeStats.damageButton.collider.enabled = false;
                upgradeStats.damageButton.GetComponent<UIButton>().isEnabled = false;
            }
            else
            {
                //upgradeStats.damageButton.collider.enabled = true;
                upgradeStats.damageButton.GetComponent<UIButton>().isEnabled = true;
            }

            if(knowledge_int < gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownLevels[gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownCurrentLevel+1].price)
            {
                //upgradeStats.cooldownButton.collider.enabled = false;
                upgradeStats.cooldownButton.GetComponent<UIButton>().isEnabled = false;
            }
            else
            {
                //upgradeStats.cooldownButton.collider.enabled = true;
                upgradeStats.cooldownButton.GetComponent<UIButton>().isEnabled = true;
            }

            if(knowledge_int < gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthLevels[gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthCurrentLevel+1].price)
            {
                //upgradeStats.healthButton.collider.enabled = false;
                upgradeStats.healthButton.GetComponent<UIButton>().isEnabled = false;
            }
            else
            {
                //upgradeStats.healthButton.collider.enabled = true;
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

    /*void UpgradeSelected(GameObject obj)
    {
        if (obj.name == "Upgrade_TowerCooldown")
        {
            TowerDefenseStats stats = gameDataControllerScript.towersData [currentTower].mainGameData.stats;

            int currentUpgradeLevel = stats.shootCooldownCurrentLevel;
            int numberOfLevels = stats.shootCooldownLevels.Length;
            int upgradePrice = stats.shootCooldownLevels[currentUpgradeLevel].price;

            if(currentUpgradeLevel+1 < numberOfLevels
               && knowledge_int >= upgradePrice)
            {
                knowledge_int -= upgradePrice;
                if (knowledge_int == 0)
                {
                    knowledge_string = knowledge_int.ToString();
                }
                else
                {
                    knowledge_string = knowledge_int.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
                }
                PlayerPrefs.SetInt("PlayerKnowledge", knowledge_int);

                PlayerPrefs.SetInt("CooldownLevel_" + currentTower.ToString(), currentUpgradeLevel);

                gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownCurrentLevel += 1;
            }
        }
        else if (obj.name == "Upgrade_TowerDamage")
        {
            TowerDefenseStats stats = gameDataControllerScript.towersData [currentTower].mainGameData.stats;
            
            int currentUpgradeLevel = stats.damageCurrentLevel;
            int numberOfLevels = stats.damageLevels.Length;
            int upgradePrice = stats.damageLevels[currentUpgradeLevel].price;
            
            if(currentUpgradeLevel+1 < numberOfLevels
               && knowledge_int >= upgradePrice)
            {
                knowledge_int -= upgradePrice;
                if (knowledge_int == 0)
                {
                    knowledge_string = knowledge_int.ToString();
                }
                else
                {
                    knowledge_string = knowledge_int.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
                }
                PlayerPrefs.SetInt("PlayerKnowledge", knowledge_int);

                PlayerPrefs.SetInt("DamageLevel_" + currentTower.ToString(), currentUpgradeLevel);
                
                gameDataControllerScript.towersData [currentTower].mainGameData.stats.damageCurrentLevel += 1;
            }
        }
        else if (obj.name == "Upgrade_TowerHealth")
        {
            TowerDefenseStats stats = gameDataControllerScript.towersData [currentTower].mainGameData.stats;
            
            int currentUpgradeLevel = stats.healthCurrentLevel;
            int numberOfLevels = stats.healthLevels.Length;
            int upgradePrice = stats.healthLevels[currentUpgradeLevel].price;
            
            if(currentUpgradeLevel+1 < numberOfLevels
               && knowledge_int >= upgradePrice)
            {
                knowledge_int -= upgradePrice;
                if (knowledge_int == 0)
                {
                    knowledge_string = knowledge_int.ToString();
                }
                else
                {
                    knowledge_string = knowledge_int.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
                }
                PlayerPrefs.SetInt("PlayerKnowledge", knowledge_int);

                PlayerPrefs.SetInt("HealthLevel_" + currentTower.ToString(), currentUpgradeLevel);
                
                gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthCurrentLevel += 1;
            }
        }

        UpdateGUI();
    }*/

    /*public string UpgradeHover(GameObject obj)
    {
        string result = "Error";

        if (obj.name == "Upgrade_TowerCooldown")
        {
            int currentUpgradeLevel = gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownCurrentLevel;
            int numberOfLevels = gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownLevels.Length;

            if(currentUpgradeLevel+1 >= numberOfLevels)
            {
                result = "MAX";
            }
            else
            {
                float upgradePrice = gameDataControllerScript.towersData [currentTower].mainGameData.stats.shootCooldownLevels[currentUpgradeLevel+1].price;
                result = "Price: " + upgradePrice.ToString();
            }
        }
        else if (obj.name == "Upgrade_TowerDamage")
        {
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
            }
        }
        else if (obj.name == "Upgrade_TowerHealth")
        {
            int currentUpgradeLevel = gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthCurrentLevel;
            int numberOfLevels = gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthLevels.Length;
            
            if(currentUpgradeLevel+1 >= numberOfLevels)
            {
                result = "MAX";
            }
            else
            {
                float upgradePrice = gameDataControllerScript.towersData [currentTower].mainGameData.stats.healthLevels[currentUpgradeLevel+1].price;
                result = "Price: " + upgradePrice.ToString();
            }
        }

        return result;
    }*/

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
        }

        upgradeStats.damageButton.transform.Find("Label").GetComponent<UILabel>().text = result;
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
    }

    void OnHoverCooldown()
    {
    }
}
