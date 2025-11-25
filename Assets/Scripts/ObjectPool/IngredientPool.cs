using System.Collections.Generic;
using UnityEngine;

public class IngredientPool
{
    private GameObject _prefab;
    private int _firstInit = 2;

    private Stack<GameObject> pool = new Stack<GameObject>();

    public IngredientPool(GameObject prefab)
    {
        _prefab = prefab;
        Init();
    }
    
    public GameObject GetIngredient() //Transform parent, Vector2 pos
    {
        GameObject ing;
        if (pool.Count > 0)
        {
            ing = WakeUp();
        }
        else
        {
            ing = Create();            
        }
        //ing.transform.parent = parent;
        //ing.transform.localPosition = pos;
        return ing;
    }    

    public void ReturnToPool(GameObject ing)
    {
        ing.SetActive(false);
        pool.Push(ing);
    }

    private void Init()
    {
        for (int i = 0; i < _firstInit; i++)
        {
            GameObject newIng = Create();
            newIng.SetActive(false);
        }
    }

    private GameObject WakeUp()
    {
        GameObject ing = pool.Pop();
        ing.SetActive(true);
        return ing;
    }

    private GameObject Create()
    {
        GameObject newIng =  MonoBehaviour.Instantiate(_prefab);
        pool.Push(newIng);
        return newIng;
    }
}
