using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetTower : MonoBehaviour 
{
    private int selectedTowerIndex = -1;
    public GameObject tile;

    public GameObject buildPanel;
    public GameObject buildButtonPrefab;
    public Vector2 buildButtonSize;
    public float buildPosX = 30f;
    public float spaceBetweenButtons = 5f;
    public Vector2 buildPosY;

    public bool buildPanelOpen = false;
    public TweenPosition buildPanelTweener;
    public TweenRotation buildPanelArrowTweener;
    public LayerMask placementLayerMask;
    
    private List<GameObject> towerBuildButtons;
    private List<Vector3> towerBuildPositions;

    private Color onColor = new Color32 (220, 135, 30, 255);
    private Color offColor = new Color32 (150, 215, 250, 255);
    private Color grayColor = new Color32 (150, 150, 150, 150);

    private GameDataController gameDataControllerScript;

    private int towerCount;

	// Use this for initialization
	void Start () 
    {
        towerCount = 0;
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        towerBuildButtons = new List<GameObject>();
        towerBuildPositions = new List<Vector3>();

        int unlockedTowers = 0;
        for (int i=0; i<gameDataControllerScript.towersData.Length; i++)
        {
            if(gameDataControllerScript.towersData[i].upgradeData.isUnlocked)
            {
                unlockedTowers++;
            }
        }

        if(unlockedTowers % 2 == 0)
        {
            for(int i=0; i<unlockedTowers; i++)
            {
                Vector3 pos;
                int sgn;
                float valY;
                if(i%2==0)
                {
                    sgn = 1;
                }
                else
                {
                    sgn = -1;
                }
                valY = sgn*((((i+2)/2)*(buildButtonSize.y/2)) + (spaceBetweenButtons/2));
                pos = new Vector3(buildPosX, valY, 0f);
                towerBuildPositions.Add(pos);
            }
        }
        else
        {
            for(int i=0; i<unlockedTowers; i++)
            {
                Vector3 pos;
                int sgn;
                float valY;
                if(i>0)
                {
                    if(i%2==0)
                    {
                        sgn = 1;
                    }
                    else
                    {
                        sgn = -1;
                    }
                    valY = sgn*(((i+1)/2)*buildButtonSize.y + spaceBetweenButtons);
                }
                else
                {
                    valY = 0;
                }
                pos = new Vector3(buildPosX, valY, 0f);
                towerBuildPositions.Add(pos);
            }
        }

        int tmp = 0;
        for (int i=0; i<gameDataControllerScript.towersData.Length; i++)
        {
            if(gameDataControllerScript.towersData[i].upgradeData.isUnlocked)
            {
                Vector3 local_pos = towerBuildPositions[tmp];
                tmp++;

                GameObject towerBuildButton = NGUITools.AddChild(buildPanel, buildButtonPrefab);
                towerBuildButtons.Add(towerBuildButton);
                towerBuildButtons[i].transform.localPosition = local_pos;
                
                towerBuildButtons[i].name = gameDataControllerScript.towersData[i].towerName;
                towerBuildButtons[i].GetComponent<UIButtonMessage>().target = gameObject;

                towerBuildButtons[i].transform.Find("Background").GetComponent<UISprite>().spriteName = gameDataControllerScript.towersData[i].mainGameData.spritename;

                string price_string;
                int price_int = gameDataControllerScript.towersData[i].mainGameData.price;
                if (price_int == 0)
                {
                    price_string = price_int.ToString();
                } else
                {
                    price_string = price_int.ToString("#,#", System.Globalization.CultureInfo.InvariantCulture);
                }

                towerBuildButtons[i].transform.Find("Label").GetComponent<UILabel>().text = price_string;
            }
        }

        UpdateGUI();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Time.timeScale != 0 && selectedTowerIndex >= 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, placementLayerMask))
            {
                if (hit.transform.tag == "Tile")
                {
                    tile = hit.transform.gameObject;
                } else
                {
                    tile = null;
                }
            } else
            {
                if (tile)
                {
                    tile = null;
                }
            }

            if (Input.GetMouseButtonDown(0) && tile != null)
            {
                TileTaken tileTakenScript = tile.GetComponent<TileTaken>();

                int playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
                if (!tileTakenScript.isTaken 
                    && playerCoins >= gameDataControllerScript.towersData [selectedTowerIndex].mainGameData.price)
                {
                    playerCoins -= gameDataControllerScript.towersData [selectedTowerIndex].mainGameData.price;
                    PlayerPrefs.SetInt("PlayerCoins", playerCoins);
                    Vector3 pos = new Vector3(tile.transform.position.x, 0.8f, tile.transform.position.z);
                    GameObject newTower = (GameObject)Instantiate(gameDataControllerScript.towersData [selectedTowerIndex].mainGameData.towerPrefab, 
                                                              pos, 
                                                              Quaternion.identity);
                    newTower.gameObject.GetComponent<Health>().startingHealth = gameDataControllerScript.towersData [selectedTowerIndex].mainGameData.stats.GetHealth();
                    newTower.gameObject.GetComponent<Shoot>().cooldown = gameDataControllerScript.towersData [selectedTowerIndex].mainGameData.stats.GetShootCooldown();
                    newTower.gameObject.GetComponent<Shoot>().projectilePrefab = gameDataControllerScript.towersData [selectedTowerIndex].mainGameData.projectilePrefab;
                    newTower.gameObject.GetComponent<Shoot>().damage = gameDataControllerScript.towersData [selectedTowerIndex].mainGameData.stats.GetDamage();
                    newTower.name = "Tower_" + towerCount.ToString();
                    towerCount++;
                    tileTakenScript.tower = newTower;
                    tileTakenScript.isTaken = true;

                    selectedTowerIndex = -1;
                }
            }
        }

        UpdateGUI();
	}

    void ToggleBuildPanel()
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        bool param;
        if (buildPanelOpen) 
        {
            param = false;
        } 
        else 
        {
            param = true;
        }
        
        buildPanelTweener.Play(param);
        
        buildPanelArrowTweener.Play(param);
        
        buildPanelOpen = param;
    }

    void SetBuildChoice(GameObject btnObj)
    {
        gameDataControllerScript.PlayAudioClip(gameDataControllerScript.sounds.menuClick);
        string btnName = btnObj.name;

        for (int i=0; i<towerBuildButtons.Count; i++)
        {
            if(towerBuildButtons[i].name == btnName)
            {
                selectedTowerIndex = i;
                break;
            }
        }
        
        UpdateGUI ();
    }

    void UpdateGUI ()
    {
        for(int i=0; i<towerBuildButtons.Count; i++)
        {
            towerBuildButtons [i].transform.Find("Background").gameObject.GetComponent<UISprite>().color = offColor;
        }

        if (selectedTowerIndex >= 0)
        {
            towerBuildButtons [selectedTowerIndex].transform.Find("Background").gameObject.GetComponent<UISprite>().color = onColor;
        }

        CheckTowersCosts ();
    }

    void CheckTowersCosts()
    {
        for (int i=0; i<towerBuildButtons.Count; i++) 
        {
            if(gameDataControllerScript.towersData[i].mainGameData.price > PlayerPrefs.GetInt("PlayerCoins", 0))
            {
                towerBuildButtons [i].transform.Find("Background").gameObject.GetComponent<UISprite>().color = grayColor;
                towerBuildButtons[i].collider.enabled = false;
            }
            else
            {
                if(selectedTowerIndex == i)
                {
                    towerBuildButtons [i].transform.Find("Background").gameObject.GetComponent<UISprite>().color = onColor;
                }
                else
                {
                    towerBuildButtons [i].transform.Find("Background").gameObject.GetComponent<UISprite>().color = offColor;
                }
                
                towerBuildButtons[i].collider.enabled = true;
            }
        }
    }
}
