using System.Collections.Generic;
using UnityEngine;

//입력이 들어오면 알맞은 풀을 만들거나 찾아서 명령 내려주기
public class IngredientPoolManager : MonoBehaviour
{
    private Dictionary<IngredientStat, IngredientPool> _pools = new Dictionary<IngredientStat, IngredientPool> ();

    
    public IPoolable Get(GameObject prefab)
    {
        IngredientStat stat = prefab.GetComponent<Ingredient>().Stat;
        if (!_pools.ContainsKey(stat))
        {
            GameObject IngredientPool = new GameObject();
            IngredientPool.transform.SetParent(transform);
            _pools[stat] = new IngredientPool(prefab, IngredientPool.transform);            
        }
        else
        {
            _pools[stat].GetIngredient();
        }
        return _pools[stat].GetIngredient();
    }

    public void Return(GameObject ing)
    {
        IngredientStat stat = ing.GetComponent<Ingredient>().Stat;
        _pools[stat].ReturnToPool(ing);
    }


}
