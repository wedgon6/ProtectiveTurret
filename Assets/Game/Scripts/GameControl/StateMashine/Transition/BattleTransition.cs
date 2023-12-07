using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTransition : GameTransition
{
    public void StartBattle()
    {
        NeedTransit = true;
    }
}
