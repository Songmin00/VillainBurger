using UnityEngine;

[CreateAssetMenu(fileName = "IngredientBase", menuName = "Scriptable Objects/IngredientBase")]
public class IngredientStat : ScriptableObject
{
    [SerializeField] private IngredientType _type;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    
    public IngredientType Type => _type;
    public string Name => _name;
    public string Description => _description;
    public float Price => _price;
}
