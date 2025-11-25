using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private BurgerMenu _menu;
    [SerializeField] private MyBurger _myBurger;
    public List<BurgerRecipe> OrderList {  get; set; }
    public int OrderCount { get; set; } = 4;
  


    private void Awake()
    {
        OrderList = new List<BurgerRecipe>();
        for (int i = 0; i < OrderCount; i++)
        {
            OrderList.Add(_menu.AllBurgers[i]);
        }
    }

    //제출한 버거가 어떤 종류에 해당하는지 찾아주는 매서드.
    //버거 제출시 가장 먼저 호출할 것
    public BurgerRecipe IdentifyBurger()
    {
        Dictionary<IngredientStat, int>  myDic = TransIntoDictionary(_myBurger.BurgerRecipe);
        foreach (var burger in OrderList)
        {
            Dictionary<IngredientStat, int>  orderDic = TransIntoDictionary(burger);
            
            if(IsSame(myDic, orderDic))
            {
                return burger;
            }
        }
        return null;
    }



    //버거 레시피의 재료 리스트들을 비교에 용이한 Dictionary<재료, 개수> 형태로 변환하는 지역함수
    private Dictionary<IngredientStat, int> TransIntoDictionary(BurgerRecipe burger)
    {
        Dictionary<IngredientStat, int> dic = new Dictionary<IngredientStat, int>();

        for (int i = 0; i < burger.Patty.Count; i++)
        {
            if (dic.ContainsKey(burger.Patty[i]))
            {
                dic[burger.Patty[i]]++;
            }
            else
            {
                dic.Add(burger.Patty[i], 1);
            }                
        }
        for (int i = 0; i < burger.Sauce.Count; i++)
        {
            if (dic.ContainsKey(burger.Sauce[i]))
            {
                dic[burger.Sauce[i]]++;
            }
            else
            {
                dic.Add(burger.Sauce[i], 1);
            }
        }
        for (int i = 0; i < burger.Topping.Count; i++)
        {
            if (dic.ContainsKey(burger.Topping[i]))
            {
                dic[burger.Topping[i]]++;
            }
            else
            {
                dic.Add(burger.Topping[i], 1);
            }
        }
        for (int i = 0; i < burger.Vegetable.Count; i++)
        {
            if (dic.ContainsKey(burger.Vegetable[i]))
            {
                dic[burger.Vegetable[i]]++;
            }
            else
            {
                dic.Add(burger.Vegetable[i], 1);
            }
        }
        return dic;
    }

    // 두 딕셔너리가 동일한지 판별하는 지역함수
    private bool IsSame(Dictionary<IngredientStat, int> a, Dictionary<IngredientStat, int> b)
    {
        if (a.Count != b.Count)
        {
            return false;
        }

        foreach (var ing in a)
        {
            if (!b.TryGetValue(ing.Key, out var value))
            {
                return false;
            }
            if (ing.Value != value)
            {
                return false;
            }                        
        }
        return true;
    }
}
