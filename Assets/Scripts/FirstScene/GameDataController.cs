using UnityEngine;
using System.Collections;

[System.Serializable]
public class MiniGameData
{
    public GameObject ObjectToPlayWith;
    public Vector2[] requiredResultRanges;
    public Material backgroundMaterial;
    public int knowledgeGain;
    public int coinsGain;
}

[System.Serializable]
public class MainGameData
{
    public GameObject towerPrefab;
    public int price;
    public Texture buildButtonTexture;
}

[System.Serializable]
public class TowerData
{
    public MiniGameData miniGameData;
    public MainGameData mainGameData;
}

[System.Serializable]
public class Level
{
    public string levelName;
    public string location;
    public string time;
    public string spritename;
    public int knowledgeRequired;
}

public class GameDataController : MonoBehaviour 
{
    public TowerData[] towersData;

    public GameObject levelPrefab;
    public Level[] levels;

    void Awake()
    {
        DontDestroyOnLoad (this);
    }

	// Use this for initialization
	void Start () 
    {
        Application.LoadLevel("MainMenu");
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
}
