using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*[System.Serializable]
public class Tower
{
    public GameObject towerPrefab;
    public int price;
    public UISprite buildButton;
}*/

public class SetTower : MonoBehaviour 
{
    public int selectedTowerIndex = 0;
    public GameObject tile;

    public GameObject buildPanel;
    public GameObject buildButtonPrefab;
    public int buildPosX = 30;
    public Vector2 buildPosY;

    public bool buildPanelOpen = false;
    public TweenPosition buildPanelTweener;
    public TweenRotation buildPanelArrowTweener;
    public LayerMask placementLayerMask;
    
    private List<GameObject> towerBuildButtons;

    private Color onColor = new Color32 (220, 135, 30, 255);
    private Color offColor = new Color32 (150, 215, 250, 255);
    private Color grayColor = new Color32 (150, 150, 150, 150);

    private GameDataController gameDataControllerScript;

	// Use this for initialization
	void Start () 
    {
        gameDataControllerScript = GameObject.FindGameObjectWithTag("GameDataController").GetComponent<GameDataController>();

        towerBuildButtons = new List<GameObject>();

        int unlockedTowers = 0;
        for (int i=0; i<gameDataControllerScript.towersData.Length; i++)
        {
            if(gameDataControllerScript.towersData[i].upgradeData.isUnlocked)
            {
                unlockedTowers++;
            }
        }

        int tmp = unlockedTowers;
        for (int i=0; i<gameDataControllerScript.towersData.Length; i++)
        {
            if(gameDataControllerScript.towersData[i].upgradeData.isUnlocked)
            {
                Vector3 local_pos = Vector3.zero;
                if(unlockedTowers == 1)
                {
                    //Debug.Log(buildPosY.y - buildPosY.x);
                    //Debug.Log((buildPosY.y - buildPosY.x)/2);
                    local_pos = new Vector3(buildPosX, (buildPosY.y + buildPosY.x)/2, 0);
                }
                else if(unlockedTowers == 2)
                {
                    local_pos = new Vector3(buildPosX, buildPosY.x + ((tmp-1) * (buildPosY.y - buildPosY.x)), 0);
                    tmp--;
                }

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 20))
        if (Physics.Raycast(ray, out hit, 1000, placementLayerMask))
        {
            //Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Tile")
            {
                tile = hit.transform.gameObject;
            } else
            {
                tile = null;
            }
        } 
        else
        {
            if(tile)
            {
                tile = null;
            }
        }

        if (Input.GetMouseButtonDown(0) && tile != null)
        {
            TileTaken tileTakenScript = tile.GetComponent<TileTaken>();

            int playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
            if(!tileTakenScript.isTaken 
               && playerCoins >= gameDataControllerScript.towersData[selectedTowerIndex].mainGameData.price)
            {
                playerCoins -= gameDataControllerScript.towersData[selectedTowerIndex].mainGameData.price;
                PlayerPrefs.SetInt("PlayerCoins", playerCoins);
                Vector3 pos = new Vector3(tile.transform.position.x, 0.8f, tile.transform.position.z);
                GameObject newTower = (GameObject)Instantiate(gameDataControllerScript.towersData[selectedTowerIndex].mainGameData.towerPrefab, 
                                                              pos, 
                                                              Quaternion.identity);
                newTower.gameObject.GetComponent<Health>().health = gameDataControllerScript.towersData[selectedTowerIndex].mainGameData.stats.GetHealth();
                newTower.gameObject.GetComponent<Shoot>().cooldown = gameDataControllerScript.towersData[selectedTowerIndex].mainGameData.stats.GetShootCooldown();
                newTower.gameObject.GetComponent<Shoot>().projectilePrefab = gameDataControllerScript.towersData[selectedTowerIndex].mainGameData.projectilePrefab;
                newTower.gameObject.GetComponent<Shoot>().damage = gameDataControllerScript.towersData[selectedTowerIndex].mainGameData.stats.GetDamage();

                tileTakenScript.tower = newTower;
                tileTakenScript.isTaken = true;
            }
        }

        UpdateGUI ();
	}

    void ToggleBuildPanel()
    {
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
        
        towerBuildButtons [selectedTowerIndex].transform.Find("Background").gameObject.GetComponent<UISprite>().color = onColor;
        //turrets [structureIndex].costText.text = "$" + turrets [structureIndex].cost;

        CheckTowersCosts ();
    }

    void CheckTowersCosts()
    {
        for (int i=0; i<towerBuildButtons.Count; i++) 
        {
            if(gameDataControllerScript.towersData[i].mainGameData.price > PlayerPrefs.GetInt("PlayerCoins", 0))
            {
                //turrets[i].costText.color = Color.red;
                towerBuildButtons [i].transform.Find("Background").gameObject.GetComponent<UISprite>().color = grayColor;
                towerBuildButtons[i].collider.enabled = false;
            }
            else
            {
                //turrets[i].costText.color = Color.green;
                
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
