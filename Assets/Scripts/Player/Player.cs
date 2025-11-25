using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : SingleTon<Player>
{
    public int Money { get; set; } = 100;
    public int Essence { get; set; } = 100;
    public int Level { get; set; } = 1;

    public Dictionary<IngredientStat, int> Storage { get; set; } = new Dictionary<IngredientStat, int>();

    protected override void Awake()
    {
        base.Awake();
        // 제이슨으로 돈, 정수, 레벨 할당하기

    }



}
