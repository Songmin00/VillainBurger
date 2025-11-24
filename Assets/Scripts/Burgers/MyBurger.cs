using System.Collections.Generic;
using UnityEngine;

public class MyBurger : MonoBehaviour
{
    private IngredientStat _currentStat;
    public List<IngredientStat> Pattys { get; set; }
    public List<IngredientStat> Sauces { get; set; }
    public List<IngredientStat> Toppings { get; set; }
    public List<IngredientStat> Vegetables { get; set; }

    private void Awake()
    {
        Pattys = new List<IngredientStat>();
        Sauces = new List<IngredientStat>();
        Toppings = new List<IngredientStat>();
        Vegetables = new List<IngredientStat>();
    }

    public void AddIngredient(IngredientStat stat)
    {
        switch (stat.Type)
        {
            case IngredientType.Patty:
                Pattys.Add(stat);
                _currentStat = stat;
                break;
            case IngredientType.Sauce:
                _currentStat = stat;
                Sauces.Add(stat);
                break;
            case IngredientType.Topping:
                _currentStat = stat;
                Toppings.Add(stat);
                break;
            case IngredientType.Vegetable:
                _currentStat = stat;
                Vegetables.Add(stat);
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
                Pattys.Remove(_currentStat);
                break;
            case IngredientType.Sauce:
                Sauces.Remove(_currentStat);
                break;
            case IngredientType.Topping:
                Toppings.Remove(_currentStat);
                break;
            case IngredientType.Vegetable:
                Vegetables.Remove(_currentStat);
                break;
            default:
                break;
        }
    }
}
