
using System.Collections.Generic;
using UnityEngine;

//입력이 들어오면 알맞은 풀을 만들거나 찾아서 명령 내려주기
public class PoolManager : MonoBehaviour
{
    Dictionary<IngredientStat, IngredientPool> _pools;
    private IngredientStat _currentKey;

    private void Awake()
    {
        _pools = new Dictionary<IngredientStat, IngredientPool> ();
    }

    public GameObject GetIngredient(IngredientStat stat, GameObject prefab)
    {
        _currentKey = stat;
        if (!_pools.ContainsKey(stat))
        {
            return MakePool(stat, prefab).GetIngredient();
        }
        else
        {
            return _pools[stat].GetIngredient();
        }
    }

    public void ReturnIngredient(GameObject ing)
    {
        _pools[_currentKey].ReturnToPool(ing);
    }



    private IngredientPool MakePool(IngredientStat stat, GameObject prefab)
    {
        IngredientPool newPool = new IngredientPool(prefab);
        _pools.Add(stat, newPool);
        return newPool;
    }
}
