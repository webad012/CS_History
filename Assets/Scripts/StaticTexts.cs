using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticTexts
{
    static private string[] storyLevel1 = {"The abacus\nthe first known calculator", 
        "Was probably invented by the Babylonians as an aid to simple arithmetic around this time period", 
        "This laid the foundations for positional notation and later computing developments"};
    static private string[] storyLevel2 = {"Text4", "Text5", "Text6", "Text7"};
    public List<string[]> storyTexts = new List<string[]>(){storyLevel1, storyLevel2};

    protected StaticTexts(){}
    private static StaticTexts _instance = null;
    public static StaticTexts Instance
    {
        get
        {
            return StaticTexts._instance == null ? new StaticTexts() : StaticTexts._instance;
        }
    }
}
