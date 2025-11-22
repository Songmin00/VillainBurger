using System;
using UnityEngine;

public class IngredientPlace : MonoBehaviour
{
    [SerializeField] MinigameManager _manager;
    public bool IsPlaced { get; set; }   
    

    public void GetPlaceMode()
    {
        if (transform.childCount > 0)
        {
            IsPlaced = true;
        }
        else
        {
            IsPlaced = false;
        }
    }    
}
