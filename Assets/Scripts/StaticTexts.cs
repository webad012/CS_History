using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticTexts 
{
    public string web_api_location = "http://alas.matf.bg.ac.rs/~mi08204/projekti/CS_History/cshistoryapi.php";

    /// <summary>
    ///  main menu
    /// </summary>
    public string[] language_Play = new string[2]{"Play", "Igraj"};
    public string[] language_Options = new string[2]{"Options", "Opcije"};
    public string[] language_Exit = new string[2]{"Exitt", "Izadji"};

    /// <summary>
    /// options
    /// </summary>
    public string[] language_ResetAll = new string[2]{"Reset All", "Resetuj Sve"};

    /// <summary>
    /// upgrade
    /// </summary>
    public string[] language_Max = new string[2]{"MAX", "MAX"};
    public string[] language_Locked = new string[2]{"Locked", "Zakljucano"};
    public string[] language_NaN = new string[2]{"NaN", "NaN"};
    public string[] language_Damage = new string[2]{"Damage", "Jačina"};
    public string[] language_Cooldown = new string[2]{"Cooldown", "Hladjenje"};
    public string[] language_Health = new string[2]{"Health", "Zdravlje"};

    /// <summary>
    /// mini game
    /// </summary>
    public string[] language_Score = new string[2]{"Health", "Zdravlje"};
    public string[] language_Required = new string[2]{"Health", "Zdravlje"};

    public string[] language_Back = new string[2]{"Back", "Nazad"};
    public string[] language_Upgrade = new string[2]{"Upgrade", "Unapredi"};

    static private string[] storyTextLevel1 = {
        "Welcome!",
        "Main currency for advancing through history and upgrading your weaponry is knowledge.",
        "Yes, knowledge! Most valuable posession in the world.",
        "Each level will teach you about small steps that affected history", 
        "into developing computer science as we know it today.",
        "Each level is divided into 2 parts: minigame and defense game.",
        "In minigame you have to prove yourself worthy of obtaining knowledge of specific item for that level.",
        "After completing minigame you can pass to main game where your objective will be to defend",
        "and gain knowledge needed for next level",
        "Our story begin in ancient Sumeria.", 
        "The abacus\nthe first known calculator", 
        "Was probably invented by the Babylonians as an aid to simple arithmetic around this time period.", 
        "This laid the foundations for positional notation and later computing developments.",
        "Abacus is split into two basic rows:\ntop row is for '5's, and\nthe bottom row is for the 'ones'",
        "Each column, looking from right to left, equals column number (starting from 0) times 10",
        "Example for 100",
        "Example for 1,234,567",
        "Your objective is to create requested numbers using abacus."
    };
    static private string[] storyTextLevel2 = {
        "The south-pointing chariot", 
        "Invented in ancient China", 
        "It was the first known geared mechanism to use a differential gear.",
        "The chariot was a two-wheeled vehicle",
        "upon which is a pointing figure connected to the wheels by means of differential gearing",
        "Through careful selection of wheel size, track and gear ratios",
        "the figure atop the chariot always pointed in the same direction.",
        "Your objective is to, using scales, rotate chariot to point to south represented by green ball."
    };
    static private string[] storyTextLevel3 = {
        "Binary number system", 
        "Described by Indian mathematician/scholar/musician Pingala", 
        "Now used in the design of essentially all modern computing equipment",
        "A binary number is a number expressed in the binary numeral system, or base-2 numeral system",
        "which represents numeric values using two different symbols: typically 0 (zero) and 1 (one).",
        "More specifically, the usual base-2 system is a positional notation with a radix of 2.",
        "Each digit is referred to as a bit.",
        "Decimal vs Binary\nHere are some equivalent values",
        "Your objective is to create requested numbers using binary system."
    };
    public List<string[]> storyTexts = new List<string[]>(){storyTextLevel1, storyTextLevel2, storyTextLevel3};

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
