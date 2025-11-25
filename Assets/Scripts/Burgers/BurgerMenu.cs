using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurgerMenu", menuName = "Scriptable Objects/BurgerMenu")]
public class BurgerMenu : ScriptableObject
{
    private List<BurgerRecipe> _allBurgers;

    public List<BurgerRecipe> AllBurgers => _allBurgers;
}
