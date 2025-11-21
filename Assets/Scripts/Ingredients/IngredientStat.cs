using UnityEngine;

[CreateAssetMenu(fileName = "IngredientBase", menuName = "Scriptable Objects/IngredientBase")]
public class IngredientStat : ScriptableObject
{
    [SerializeField] private IngredientType _type;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private string _buffName;
    [SerializeField] private float _buffValue;
}
