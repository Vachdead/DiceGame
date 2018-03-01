using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dice Scipt
[System.Serializable]
public class Dice
{
    public int number;

    public bool selected;
    public bool used;

    public Dice (int n, bool s, bool u)
    {
        number = n;
        selected = s;
        used = u;
    }
}
