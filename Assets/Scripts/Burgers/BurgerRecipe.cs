using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burger", menuName = "Scriptable Objects/Burger")]
public class BurgerRecipe : ScriptableObject
{
    [SerializeField] private BurgerType _type;
    [SerializeField] private string _name = "¹ö°Å";
    [SerializeField] private List<IngredientStat> _patty;
    [SerializeField] private List<IngredientStat> _sauce;       
    [SerializeField] private List<IngredientStat> _topping;
    [SerializeField] private List<IngredientStat> _vegetable;

    public BurgerType Type => _type;
    public string Name => _name;
    public List<IngredientStat> Patty => _patty;
    public List<IngredientStat> Sauce => _sauce;
    public List<IngredientStat> Topping => _topping;
    public List<IngredientStat> Vegetable => _vegetable;

}
