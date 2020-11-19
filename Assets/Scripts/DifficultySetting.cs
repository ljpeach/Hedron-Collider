using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DifficultySetting
{
    public static int difficulty;

    public static int Difficulty
    {
        get
        {
            return difficulty;
        }
        set
        {
            difficulty = value;
        }
    }
}
