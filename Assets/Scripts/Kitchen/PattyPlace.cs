using System;
using UnityEngine;

public class PattyPlace : IngredientPlace
{
    public override void PlayMinigame()
    {
        _manager.SetState(new PattyMinigame(_manager));
    }
}
