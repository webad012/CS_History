using UnityEngine;
using System.Collections;

[System.Serializable]
public class Tower
{
    public GameObject towerPrefab;
    public int price;
    public UISprite buildButton;
}

public class SetTower : MonoBehaviour 
{
    public int selectedTowerIndex = 0;
    public Tower[] towers;
    public GameObject tile;
    private Money moneyScript;

    public bool buildPanelOpen = false;
    public TweenPosition buildPanelTweener;
    public TweenRotation buildPanelArrowTweener;
    public UISprite xMark;

	// Use this for initialization
	void Start () 
    {
        moneyScript = GameObject.Find("GameLogic").GetComponent<Money>();

        UpdateGUI();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20))
        {
            if(hit.transform.tag == "Tile")
            {
                tile = hit.transform.gameObject;
            }
            else
            {
                tile = null;
            }
        }

        if (Input.GetMouseButtonDown(0) && tile != null)
        {
            TileTaken tileTakenScript = tile.GetComponent<TileTaken>();

            if(!tileTakenScript.isTaken && moneyScript.money >= towers[selectedTowerIndex].price)
            {
                moneyScript.money -= towers[selectedTowerIndex].price;
                Vector3 pos = new Vector3(tile.transform.position.x, 1f, tile.transform.position.z);
                tileTakenScript.tower = (GameObject)Instantiate(towers[selectedTowerIndex].towerPrefab, 
                                                                pos, 
                                                                Quaternion.identity);
                tileTakenScript.isTaken = true;
            }
        }
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
        
        if (btnName == "Button_TowerIncome") 
        {
            selectedTowerIndex = 0;
        }
        else if (btnName == "Button_TowerShoot") 
        {
            selectedTowerIndex = 1;
        }
        else if(btnName == "Button_TowerExpensive")
        {
            selectedTowerIndex = 2;
        }
        
        UpdateGUI ();
    }

    void UpdateGUI ()
    {
        Vector3 pos = new Vector3(towers [selectedTowerIndex].buildButton.transform.position.x, 
                                  towers [selectedTowerIndex].buildButton.transform.position.y, 
                                  towers [selectedTowerIndex].buildButton.transform.position.z);
        xMark.transform.position = pos;
    }
}
