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
    public float optionTransitionSpeed = 0.5f;
    public UILabel towerNameLabel;
    public UILabel knowledgeValueLabel;
    public UILabel backButtonLabel;
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

    private int selectedLanguage;

	// Use this for initialization
	void Start () 
    {
        selectedLanguage = PlayerPrefs.GetInt("SelectedLanguage", 0);

        upgradeStats.damageButton.GetComponent<UIButton>().isEnabled = false;
        upgradeStats.cooldownButton.GetComponent<UIButton>().isEnabled = false;
        upgradeStats.healthButton.GetComponent<UIButton>().isEnabled = false;

        upgradeStats.healthAdditionLabel.text = "";
        upgradeStats.damageAdditionLabel.text = "";
        upgradeStats.cooldownAdditionLabel.text = "";

        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        backButtonLabel.text = StaticTexts.Instance.language_Back [selectedLanguage];

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

        damageInfoString = StaticTexts.Instance.language_Damage[selectedLanguage] + ": ";
        cooldowInfoString = StaticTexts.Instance.language_Cooldown[selectedLanguage] + ": ";
        healthInfoString = StaticTexts.Instance.language_Health[selectedLanguage] + ": ";

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
                damageButtonString = StaticTexts.Instance.language_Max[selectedLanguage];
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
                damageButtonString = StaticTexts.Instance.language_Upgrade[selectedLanguage] + ": " + towerStats.damageLevels[towerStats.damageCurrentLevel+1].price.ToString();
            }

            if(towerStats.shootCooldownCurrentLevel+1 >= towerStats.shootCooldownLevels.Length)
            {
                upgradeStats.cooldownButton.GetComponent<UIButton>().isEnabled = false;
                cooldowButtonString = StaticTexts.Instance.language_Max[selectedLanguage];
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
                cooldowButtonString = StaticTexts.Instance.language_Upgrade[selectedLanguage] + ": " + towerStats.shootCooldownLevels[towerStats.shootCooldownCurrentLevel+1].price.ToString();
            }

            if(towerStats.healthCurrentLevel+1 >= towerStats.healthLevels.Length)
            {
                upgradeStats.healthButton.GetComponent<UIButton>().isEnabled = false;
                healthButtonString = StaticTexts.Instance.language_Max[selectedLanguage];
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
                healthButtonString = StaticTexts.Instance.language_Upgrade[selectedLanguage] + ": " + towerStats.healthLevels[towerStats.healthCurrentLevel+1].price.ToString();
            }
        }
        else
        {
            towerName = StaticTexts.Instance.language_Locked[selectedLanguage];
            
            damageInfoString += StaticTexts.Instance.language_NaN[selectedLanguage];
            cooldowInfoString += StaticTexts.Instance.language_NaN[selectedLanguage];
            healthInfoString += StaticTexts.Instance.language_NaN[selectedLanguage];

            damageButtonString = StaticTexts.Instance.language_Locked[selectedLanguage];
            cooldowButtonString = StaticTexts.Instance.language_Locked[selectedLanguage];
            healthButtonString = StaticTexts.Instance.language_Locked[selectedLanguage];
            
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
    }

    void ButtonLeft()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
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
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
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
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        Application.LoadLevel("MainMenu");
    }

    void ButtonDamage()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
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
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
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
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
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
