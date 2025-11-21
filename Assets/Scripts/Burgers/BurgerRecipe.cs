using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burger", menuName = "Scriptable Objects/Burger")]
public class BurgerRecipe : ScriptableObject
{
    [SerializeField] private string _name = "¹ö°Å";
    [SerializeField] private List<IngredientStat> _patty;
    [SerializeField] private List<IngredientStat> _sauces;       
    [SerializeField] private List<IngredientStat> _topping;
    [SerializeField] private List<IngredientStat> _vegetables;

}
