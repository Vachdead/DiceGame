using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    //GO Dice
    public GameObject diceGO;

    // Images Dice
    public Sprite diceSpriteNormal;
    public Sprite diceSpritesSelected;
    public Sprite diceSpriteUsed;

    // Canvas - UI Handler
    public Canvas canvas;

    // Number of Dices ingame
    private int NumberofDices = 6;

    // Point Variable
    private int currentRoundPoints;
    private int currentRoundPointsChecker;
    private int lastRoundPoints;
    private int allPoints;
    private int round;

    // Check how much dices each found
    private int CounterofDicewithOne;
    private int CounterofDicewithTwo;
    private int CounterofDicewithThree;
    private int CounterofDicewithFour;
    private int CounterofDicewithFive;
    private int CounterofDicewithSix;

    // confirm pressed
    private bool confirmSelection = true;

    // Button UI
    public Button[] diceButton;
    public Button confirmButton;
    public Button roleTheDice;
    public Button endRound;


    //Vector3[] spawnPlaces = new[] { new Vector3(-6, 0, 0), new Vector3(-4, 0, 0), new Vector3(-2, 0, 0), new Vector3(2, 0, 0), new Vector3(4, 0, 0), new Vector3(6, 0, 0), };

    // Dice list      
    public List<Dice> dices = new List<Dice>();

    // Use this for initialization
    void Start () {
        StartGame(); // activate Start Game
    }

    /*private void AddDicesToList ()
    {
        for (int i = 0; i < NumberofDices; i++)
        {
            dices.Add(new Dice(0, false, false));
        }
    }*/

    // Start Game
    private void StartGame ()
    {        
        // roll the dice
        RollTheDice();

        // Write UI Points
        GameObject.Find("CPoints").GetComponent<Text>().text = currentRoundPoints.ToString();
        GameObject.Find("LRPoints").GetComponent<Text>().text = lastRoundPoints.ToString();
        GameObject.Find("APoints").GetComponent<Text>().text = allPoints.ToString();
        GameObject.Find("RD").GetComponent<Text>().text = (round + 1).ToString();

        // Listener for Dice Button
        for (int i = 0; i < NumberofDices; i++)
        {
            diceButton[i].onClick.AddListener(SelectDeselect);
        }
    }

    /*
    public void SpawnDices(int amount)
    {
        GameObject[] dicesGO = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            dicesGO[i] = (GameObject)Instantiate(diceGO, spawnPlaces[i], Quaternion.identity);
            dicesGO[i].name = "Dice " + (i + 1);
        }
    }*/

    // Roll Dices a number
    public void RollTheDice ()
    {      
        if (confirmSelection == true) // Check if confirm pressed - player is online allowed to role dices once
        {
            roleTheDice.interactable = false; // deactivate dice
            confirmSelection = false; // reset confirm pressed

            // roll each dice
            for (int i = 0; i < NumberofDices; i++)
            {
                if (diceButton[i].GetComponent<DiceManager>().dice.selected == false)
                {
                    dices[i].number = Random.Range(1, 7); // Random number for dice
                    GameObject go = GameObject.Find("Dice " + (i + 1));  
                    go.GetComponentInChildren<Text>().text = dices[i].number.ToString(); // Write UI Dice Number
                }
            }
        }            
    }

    // Select - Deselect Button
    public void SelectDeselect()
    {
       
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>(); // Check if button pressed
        button.GetComponent<DiceManager>().dice.selected = !button.GetComponent<DiceManager>().dice.selected; // change pressed button to selected

        // Change Image of Dice
        if (button.GetComponent<DiceManager>().dice.selected == true)
        {
            button.GetComponent<Button>().image.overrideSprite = diceSpritesSelected;
        }
        else
        {
            button.GetComponent<Button>().image.overrideSprite = diceSpriteNormal;
        }
    }

    // Confirm Selection
    public void ConfirmButton ()
    {        
        confirmSelection = true;
        roleTheDice.interactable = true; // allow to roll the dice

        // Selected dices chnage to interactable and change Image
        for (int i = 0; i < NumberofDices; i++)
        {
            if (diceButton[i].GetComponent<DiceManager>().dice.selected == true)
            {
                diceButton[i].GetComponent<Button>().image.overrideSprite = diceSpriteUsed;
                diceButton[i].interactable = false;
            }
        }
        
    }

    // Read Points
    public void ReadPoints ()
    {
        currentRoundPointsChecker = currentRoundPoints; // Check points and current points equal

        // Counter equal
        CounterofDicewithOne = 0;
        CounterofDicewithTwo = 0;
        CounterofDicewithThree = 0;
        CounterofDicewithFour = 0;
        CounterofDicewithFive = 0;
        CounterofDicewithSix = 0;

        // Read all selected and unused Dice
        for (int i = 0; i < NumberofDices; i++)
        {
            if (diceButton[i].GetComponent<DiceManager>().dice.selected == true && dices[i].used == false)
            {
                dices[i].used = true;

                // Hardcoded Point Reader
                switch (dices[i].number)
                {
                    case 1:
                        CounterofDicewithOne++;

                        // Read Points with One
                        if (CounterofDicewithOne < 3)
                        {
                            currentRoundPoints += 100 * CounterofDicewithOne;
                            CounterofDicewithOne = 0;
                        }
                        else if (CounterofDicewithOne == 3)
                        {
                            currentRoundPoints += 1000 * 1;
                        }
                        else if (CounterofDicewithOne == 4)
                        {
                            currentRoundPoints += 1000 * 2;
                        }
                        else if (CounterofDicewithOne == 5)
                        {
                            currentRoundPoints += 1000 * 4;
                        }
                        else if (CounterofDicewithOne == 6)
                        {
                            currentRoundPoints += 1000 * 8;
                        }
                        break;
                    case 2:
                        CounterofDicewithTwo++;

                        // Read Points with Two
                        if (CounterofDicewithTwo == 3)
                        {
                            currentRoundPoints += 200 * 1;
                        }
                        else if (CounterofDicewithTwo == 4)
                        {
                            currentRoundPoints += 200 * 2;
                        }
                        else if (CounterofDicewithTwo == 5)
                        {
                            currentRoundPoints += 200 * 4;
                        }
                        else if (CounterofDicewithTwo == 6)
                        {
                            currentRoundPoints += 200 * 8;
                        }
                        break;
                    case 3:
                        CounterofDicewithThree++;

                        // Read Points with Three
                        if (CounterofDicewithThree == 3)
                        {
                            currentRoundPoints += 300 * 1;
                        }
                        else if (CounterofDicewithThree == 4)
                        {
                            currentRoundPoints += 300 * 2;
                        }
                        else if (CounterofDicewithThree == 5)
                        {
                            currentRoundPoints += 300 * 4;
                        }
                        else if (CounterofDicewithThree == 6)
                        {
                            currentRoundPoints += 300 * 8;
                        }
                        break;
                    case 4:
                        CounterofDicewithFour++;

                        // Read Points with Four
                        if (CounterofDicewithFour == 3)
                        {
                            currentRoundPoints += 400 * 1;
                        }
                        else if (CounterofDicewithFour == 4)
                        {
                            currentRoundPoints += 400 * 2;
                        }
                        else if (CounterofDicewithFour == 5)
                        {
                            currentRoundPoints += 400 * 4;
                        }
                        else if (CounterofDicewithFour == 6)
                        {
                            currentRoundPoints += 400 * 8;
                        }
                        break;
                    case 5:
                        CounterofDicewithFive++;

                        // Read Points with Five
                        if (CounterofDicewithFive < 3)
                        {
                            currentRoundPoints += 50 * CounterofDicewithFive;
                            CounterofDicewithFive = 0;
                        }
                        else if (CounterofDicewithFive == 3)
                        {
                            currentRoundPoints += 500 * 1;
                        }
                        else if (CounterofDicewithFive == 4)
                        {
                            currentRoundPoints += 500 * 2;
                        }
                        else if (CounterofDicewithFive == 5)
                        {
                            currentRoundPoints += 500 * 4;
                        }
                        else if (CounterofDicewithFive == 6)
                        {
                            currentRoundPoints += 500 * 8;
                        }
                        break;
                    case 6:
                        CounterofDicewithSix++;

                        // Read Points with Six
                        if (CounterofDicewithSix == 3)
                        {
                            currentRoundPoints += 600 * 1;
                        }
                        else if (CounterofDicewithSix == 4)
                        {
                            currentRoundPoints += 600 * 2;
                        }
                        else if (CounterofDicewithSix == 5)
                        {
                            currentRoundPoints += 600 * 4;
                        }
                        else if (CounterofDicewithSix == 6)
                        {
                            currentRoundPoints += 600 * 8;
                        }
                        break;
                }                            
            } 
        }

        // Check if you lost (got 0 Points)
        // lose - Start Game and Reset variables
        if (currentRoundPoints == currentRoundPointsChecker)
        {
            currentRoundPoints = 0;
            allPoints = 0;
            round = 0;
            RollTheDice();
            EndRound();
        }
        // won
        else
        {
            currentRoundPointsChecker += currentRoundPoints;
        }
        // Add Points to UI
        GameObject.Find("CPoints").GetComponent<Text>().text = currentRoundPoints.ToString();
    }

    // End Round
    public void EndRound()
    {
        lastRoundPoints = currentRoundPoints; // Write last round points
        currentRoundPoints = 0; // Reset variable
        currentRoundPointsChecker = 0; // Reset variable
        allPoints += lastRoundPoints; // Add to all Points

        // All Button again activated - reset all Variable - reset Sprite
        for (int i = 0; i < NumberofDices; i++)
        {
            diceButton[i].GetComponent<DiceManager>().dice.selected = false;
            diceButton[i].GetComponent<DiceManager>().dice.used = false;

            dices[i].used = false;
            dices[i].selected = false;

            diceButton[i].GetComponent<Button>().image.overrideSprite = diceSpriteNormal;
            diceButton[i].interactable = true;
        }
        
        // Write Points to UI
        GameObject.Find("CPoints").GetComponent<Text>().text = currentRoundPoints.ToString();
        GameObject.Find("LRPoints").GetComponent<Text>().text = lastRoundPoints.ToString();
        GameObject.Find("APoints").GetComponent<Text>().text = allPoints.ToString();
        round++;
        GameObject.Find("RD").GetComponent<Text>().text = round.ToString();

        // Roll Dice - to get a new Round
        RollTheDice();
    }
}
