using System;
using UnityEngine;

public class IngredientPlace : MonoBehaviour
{
    public bool IsPlaced { get; set; }    
    
    public void SwitchPlaceMode()
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
