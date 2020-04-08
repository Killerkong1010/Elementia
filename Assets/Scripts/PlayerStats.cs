using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int TotalMoney { get; set; } //Integer holding the player's money.
    public int MonstersKilled { get; set; } // Integer holding the number of monsters killed
    public int Score { get; set; }// Integer holding the score that the player has.
}
