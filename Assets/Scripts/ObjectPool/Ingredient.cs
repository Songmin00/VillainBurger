using UnityEngine;



public class Ingredient : MonoBehaviour, IPoolable
{
    public IngredientStat Stat;
    
    public void OnCreate(Transform parent)
    {
        gameObject.transform.SetParent(parent);
    }

    //꺼낼 때
    public void OnGet()
    {
       gameObject.SetActive(true);       
    }

    //반환할 때
    public void OnReturn(Transform parent)
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(parent);
    }    
}
