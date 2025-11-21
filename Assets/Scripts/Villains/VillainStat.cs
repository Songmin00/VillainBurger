using UnityEngine;

[CreateAssetMenu(fileName = "VillainBase", menuName = "Scriptable Objects/VillainBase")]
public class VillainStat : ScriptableObject 
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] float _humanAttack;
    [SerializeField] float _buildingAttack;
    [SerializeField] float _attackSpeed;
    [SerializeField] float _speed;
    [SerializeField] float _range;
    [SerializeField] float _health;
    [SerializeField] float _defense;
    public void Attack()
    {

    }
}
