using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticTexts 
{
    public string web_api_location = "http://alas.matf.bg.ac.rs/~mi08204/projekti/CS_History/cshistoryapi.php";
    public string download_location = "http://alas.matf.bg.ac.rs/~mi08204/projekti/CS_History";

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
    public string[] language_AreYouSure = new string[2]{"Are you sure?", "Da li ste sigurni?"};
    public string[] language_Yes = new string[2]{"Yes", "Da"};
    public string[] language_No = new string[2]{"No", "Ne"};
    public string[] language_Volume = new string[2]{"Volume", "Jacina Zvuka"};

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
    public string[] language_Score = new string[2]{"Score: ", "Rezultat: "};
    public string[] language_ScoreRequired = new string[2]{"Required: ", "Potrebno: "};
    public string[] language_Great = new string[2]{"Great!", "Bravo!"};
    public string[] language_Continue = new string[2]{"Continue", "Nastavi"};
    public string[] language_Completed = new string[2]{"Completed: ", "Zavrseno: "};

    /// <summary>
    /// tower defense
    /// </summary>
    public string[] language_newWaveIn = new string[2]{"New wave in: ", "Novi talas za: "};
    public string[] language_GameLost = new string[2]{"Game Lost", "Poraz"};
    public string[] language_GameWon = new string[2]{"Completed", "Pobeda"};

    /// <summary>
    /// meni prozori
    /// </summary>
    public string[] language_Restart = new string[2]{"Restart", "Restartuj"};

    /// <summary>
    /// zajednicko za vise prozora
    /// </summary>
    public string[] language_Back = new string[2]{"Back", "Nazad"};
    public string[] language_Upgrade = new string[2]{"Upgrade", "Unapredi"};
    public string[] language_Required = new string[2]{"Required", "Potrebno"};
    public string[] language_MainMenu = new string[2]{"Main Menu", "Glavni meni"};
    public string[] language_GamePaused = new string[2]{"Game paused", "Igra pauzirana"};

    /*static private string[] storyTextLevel1 = {
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
    public List<string[]> storyTexts = new List<string[]>(){storyTextLevel1, storyTextLevel2, storyTextLevel3};*/
    static private List<string[]> storyTextLevel1 = new List<string[]>()
    {
        new string[]{
            "Welcome!", 
            "Dobro dosli!"
        },
        new string[]{
            "Main currency for advancing through history and upgrading your weaponry is knowledge.", 
            "Glavna valuta za napredak kroz istoriju i unapredjivanje oruzja je znanje."
        },
        new string[]{
            "Yes, knowledge! Most valuable possession in the world.", 
            "Da, znanje! Najvredniji imetak na svetu."
        },
        new string[]{
            "Each level will teach you about small steps that affected history", 
            "Svaki nivo ce vas nauciti o malim koracima koji su uticali na istoriju"
        },
        new string[]{
            "into developing computer science as we know it today.", 
            "u razvijanju kompjuterskih nauka kakvu je znamo danas."
        },
        new string[]{
            "Each level is divided into 2 parts: minigame and defense game.", 
            "Svaki nivo je podeljen na 2 dela: mini igru i odbrambrenu igru."
        },
        new string[]{
            "In minigame you have to prove yourself worthy of obtaining knowledge of specific item for that level.", 
            "U mini igri moras dokazati dostojnost posedovanja znanja odredjenog predmeta za taj nivo."
        },
        new string[]{
            "After completing minigame you can pass to main game where your objective will be", 
            "Nakon zavrsetka mini igre prelazite na glavnu igru gde je cilj"
        },
        new string[]{
            "to defendand gain knowledge needed for next level", 
            "da se odbrani i sakupi znanje potrebno za sledeci nivo"
        },
        new string[]{
            "Our story begin in ancient Sumeria.", 
            "Nasa prica pocinje u drevnoj Sumeriji."
        },
        new string[]{
            "The abacus\nthe first known calculator", 
            "Abakus\nprvi poznati kalkulator"
        },
        new string[]{
            "Was probably invented by the Babylonians as an aid to simple arithmetic around this time period.", 
            "Verovatno su ga izumeli Vavilonci kao pomoc u jednostavnoj aritmetici za ovaj vremenski period."
        },
        new string[]{
            "This laid the foundations for positional notation and later computing developments.", 
            "Ovo je postavilo temelje za pozicionu notaciju i razvoj racunarstva."
        },
        new string[]{
            "Abacus is split into two basic rows:\ntop row is for '5's, and\nthe bottom row is for the 'ones'", 
            "Abakus je podeljen u dva osnovna reda:\ngornji red je za 'petice', a\ndonji red je za 'jedinice'"
        },
        new string[]{
            "Each column, looking from right to left, equals column number (starting from 0) times 10", 
            "Svaka kolona, posmatrano s desna na levo, jednaka je rednom broju kolone (pocevsi od 0) na 10. stepen"
        },
        new string[]{
            "Example for 100", 
            "Primer za 100"
        },
        new string[]{
            "Example for 1,234,567", 
            "Primer za 1,234,567"
        },
        new string[]{
            "Your objective is to create requested numbers using abacus.", 
            "Vas zadatak je da napravite trazene brojeve koristeci abakus."
        }
    };

    static private List<string[]> storyTextLevel2 = new List<string[]>()
    {
        new string[]{
            "The south-pointing chariot", 
            "Kocije koje pokazuju jug"
        },
        new string[]{
            "Invented in ancient China", 
            "Izumljene u drevnoj Kini"
        },
        new string[]{
            "It was the first known geared mechanism to use a differential gear.", 
            "Bio je to prvi poznati zupcani mehanizam koji je koristio diferencijalni prenos."
        },
        new string[]{
            "The chariot was a two-wheeled vehicle", 
            "Kocije su dvo-tockasko vozilo"
        },
        new string[]{
            "upon which is a pointing figure connected to the wheels by means of differential gearing", 
            "na kojima se nalazi pokazujuca figurakonektovana sa tockovima koristeci diferencijalni prenos"
        },
        new string[]{
            "Through careful selection of wheel size, track and gear ratios", 
            "Kroz pazljivu selekciju velicine tockova, odnosa sina i zupcanika"
        },
        new string[]{
            "the figure atop the chariot always pointed in the same direction.", 
            "figura na vrhu kocija uvek pokazuje ka istom pravcu."
        },
        new string[]{
            "Your objective is to, using scales, rotate chariot to point to south represented by green ball.", 
            "Vas zadatak je da, koristeci skale, rotirate kocije da pokazuju ka jugu predstavljenom zelenom kuglom."
        }
        
        /*static private string[] storyTextLevel2 = {
            "The south-pointing chariot", 
            "Invented in ancient China", 
            "It was the first known geared mechanism to use a differential gear.",
            "The chariot was a two-wheeled vehicle",
            "upon which is a pointing figure connected to the wheels by means of differential gearing",
            "Through careful selection of wheel size, track and gear ratios",
            "the figure atop the chariot always pointed in the same direction.",
            "Your objective is to, using scales, rotate chariot to point to south represented by green ball."
        };*/
    };

    static private List<string[]> storyTextLevel3 = new List<string[]>()
    {
        new string[]{
            "Binary number system", 
            "Binarni brojni sistem"
        },
        new string[]{
            "Described by Indian mathematician/scholar/musician Pingala", 
            "Opisan od strane Indijskog matematicara/ucenjaka/muzicara Pengale"
        },
        new string[]{
            "Now used in the design of essentially all modern computing equipment", 
            "Sada koriscen u dizajnu esencijalno sve moderne kompjuterske opreme"
        },
        new string[]{
            "A binary number is a number expressed in the binary numeral system, or base-2 numeral system", 
            "Binarni broj je broj izrazen u binarnom numerickom sistemu, ili brojnom sistemu baze 2"
        },
        new string[]{
            "which represents numeric values using two different symbols: 0 (zero) and 1 (one).", 
            "koji predstavlja numericku vrednost koristeci dva razlicita simbola: 0 (nula) i 1 (jedan)"
        },
        new string[]{
            "More specifically, the usual base-2 system is a positional notation with a radix of 2.", 
            "Preciznije, uobicajeni 2-bazni sistem je poziciona notacija sa osnovom 2."
        },
        new string[]{
            "Each digit is referred to as a bit.", 
            "Svaka cifra se naziva bit."
        },
        new string[]{
            "Decimal vs Binary\nHere are some equivalent values", 
            "Decimalni protiv Binarni\nPrikazane su neke ekvivalentne vrednosti"
        },
        new string[]{
            "Your objective is to create requested numbers using binary system.", 
            "Vas zadatak je da kreirate trazene brojeve koristeci binarni sistem."
        }
        
        /*static private string[] storyTextLevel3 = {
            "Binary number system", 
            "Described by Indian mathematician/scholar/musician Pingala", 
            "Now used in the design of essentially all modern computing equipment",
            "A binary number is a number expressed in the binary numeral system, or base-2 numeral system",
            "which represents numeric values using two different symbols: typically 0 (zero) and 1 (one).",
            "More specifically, the usual base-2 system is a positional notation with a radix of 2.",
            "Each digit is referred to as a bit.",
            "Decimal vs Binary\nHere are some equivalent values",
            "Your objective is to create requested numbers using binary system."
        };*/
    };

    public List< List<string[]> > storyTexts = new List< List<string[]> >()
    {
        storyTextLevel1, storyTextLevel2, storyTextLevel3
    };

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
