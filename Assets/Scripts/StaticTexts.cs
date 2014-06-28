using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticTexts 
{

    static private string[] storyLevel1 = {
        "Welcome!",
        "Main currency for advancing through history and upgrading your weaponry is knowledge.",
        "Yes, knowledge! Most valuable posession in the world.",
        "Each level will teach you about small step that affected history", 
        "into developing computer science as we know it today.",
        "Each level is divided into 2 parts: minigame and defense game.",
        "In minigame you have to prove yourself worthy of obtaining knowledge of specific item for that level.",
        "After completing minigame you can pass to main game where your objective will be to defend",
        "and gain knowledge needed for next level",
        "Our story begin in ancient Sumeria.", 
        "The abacus\nthe first known calculator", 
        "Was probably invented by the Babylonians as an aid to simple arithmetic around this time period", 
        "This laid the foundations for positional notation and later computing developments"
    };
    static private string[] storyLevel2 = {"Binary number system", 
        "Described by Indian mathematician/scholar/musician Pingala", 
        "Now used in the design of essentially all modern computing equipment"};
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
