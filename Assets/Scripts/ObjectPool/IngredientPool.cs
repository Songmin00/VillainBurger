using System.Collections.Generic;
using UnityEngine;

public class IngredientPool
{
    private Stack<IPoolable> _pool = new Stack<IPoolable>();
    private GameObject _prefab;
    private int _firstInit = 2;
    private Transform _parent;
    
    //생성자
    public IngredientPool(GameObject prefab, Transform parent)
    {
        _prefab = prefab;
        Init();        
        _parent = parent;
    }
    



    public IPoolable GetIngredient()
    {
        IPoolable ing;
        if (_pool.Count > 0)
        {
            ing = Get();
        }
        else
        {
            ing = Create();            
        }        
        return ing;
    }    

    public void ReturnToPool(GameObject ing)
    {
        ing.GetComponent<IPoolable>().OnReturn(_parent);
        _pool.Push(ing.GetComponent<IPoolable>());
    }




    //지역함수
    private void Init()
    {
        for (int i = 0; i < _firstInit; i++)
        {
            IPoolable newIng = Create();            
        }
    }

    private IPoolable Get()
    {
        IPoolable ing = _pool.Pop();
        ing.OnGet();
        return ing;
    }

    private IPoolable Create()
    {
        GameObject newIng =  MonoBehaviour.Instantiate(_prefab);
        IPoolable poolable = newIng.GetComponent<IPoolable>();
        poolable.OnCreate(_parent);
        _pool.Push(poolable);
        return poolable;
    }
}
