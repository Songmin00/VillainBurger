using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private BurgerMenu _menu;    
    private List<BurgerRecipe> _orderList;
    private BurgerRecipe _burgerToOrder;
    private int _orderCount = 10;
    [SerializeField] MyBurger _myBurger;
    [SerializeField] StoreManager _storeManager;

    private void Awake()
    {
        for (int i = 0; i < _orderCount; i++)
        {
            _orderList.Add(_menu.AllBurgers[i]);
        }
    }

    public void OrderBurger()
    {
        MakeOrder();
        //주문 표시 매서드 호출
    }

    public void CheckRecipe()
    {
        if (_myBurger.BurgerRecipe == _storeManager.IdentifyBurger())
        {
            // 레시피 일치 이후 매서드 호출
        }    
        //레시피 불일치 이후 매서드 호출
    }



    private void MakeOrder()
    {
        int random = Random.Range(0, _orderCount -1);
        _burgerToOrder = _orderList[random];
    }

    
}
