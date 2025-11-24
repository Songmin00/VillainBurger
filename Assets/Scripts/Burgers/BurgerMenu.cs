using System.Collections.Generic;
using UnityEngine;

public class BurgerMenu : MonoBehaviour
{
    
    [SerializeField] private List<BurgerRecipe> _allBurgers;
    public List<BurgerRecipe> OrderableBurgers { get; set; }


    private void Awake()
    {
        OrderableBurgers = new List<BurgerRecipe>();
    }

    private void Start()
    {
     
    }

}
