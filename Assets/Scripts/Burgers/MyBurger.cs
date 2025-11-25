using System.Collections.Generic;
using UnityEngine;

public class MyBurger : MonoBehaviour
{
    private IngredientStat _currentStat;
   
    public BurgerRecipe BurgerRecipe { get; set; }

    private void Awake()
    {
        _currentStat = new IngredientStat();
        BurgerRecipe = new BurgerRecipe();
    }

    public void AddIngredient(IngredientStat stat)
    {
        switch (stat.Type)
        {
            case IngredientType.Patty:
                BurgerRecipe.Patty.Add(stat);
                _currentStat = stat;
                break;
            case IngredientType.Sauce:
                _currentStat = stat;
                BurgerRecipe.Sauce.Add(stat);
                break;
            case IngredientType.Topping:
                _currentStat = stat;
                BurgerRecipe.Topping.Add(stat);
                break;
            case IngredientType.Vegetable:
                _currentStat = stat;
                BurgerRecipe.Vegetable.Add(stat);
                break;
            default:
                break;
        }
        
    }

    public void RemoveIngredient()
    {
        switch (_currentStat.Type)
        {
            case IngredientType.Patty:
                BurgerRecipe.Patty.Remove(_currentStat);
                break;
            case IngredientType.Sauce:
                BurgerRecipe.Sauce.Remove(_currentStat);
                break;
            case IngredientType.Topping:
                BurgerRecipe.Topping.Remove(_currentStat);
                break;
            case IngredientType.Vegetable:
                BurgerRecipe.Vegetable.Remove(_currentStat);
                break;
            default:
                break;
        }
    }
}
