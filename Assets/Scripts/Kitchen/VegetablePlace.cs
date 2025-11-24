using UnityEngine;

public class VegetablePlace : IngredientPlace
{
    public override void PlayMinigame()
    {
        _manager.SetState(new VegetableMinigame(_manager));
    }
}
