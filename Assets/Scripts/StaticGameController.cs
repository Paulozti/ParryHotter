using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGameController
{
    private static int player1_character, player2_character; //1 For Elda and 2 For Vivi
    public static int player1
    {
        get
        {
            return player1_character;
        }
        set
        {
            player1_character = value;
        }
    }

    public static int player2
    {
        get
        {
            return player2_character;
        }
        set
        {
            player2_character = value;
        }
    }
}

