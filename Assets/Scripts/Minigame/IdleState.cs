using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : IMinigameState
{
    MinigameManager _manager;

    public IdleState(MinigameManager manager)
    {
        _manager = manager;        
    }

    public void Enter()
    {        
        _manager.PattySlider.SetActive(false);
        _manager.VegetableSlider.SetActive(false);
        _manager.CommandPannel.parent.gameObject.SetActive(false);
    }

    public void Exit()
    {
        
    }

    public void GetDownArrow(InputAction.CallbackContext ctx)
    {
        
    }

    public void GetLeftArrow(InputAction.CallbackContext ctx)
    {
        
    }

    public void GetRightArrow(InputAction.CallbackContext ctx)
    {
        
    }
    
    public void GetUpArrow(InputAction.CallbackContext ctx)
    {
        
    }

    //디버깅용 최근 재료 지우기 기능
    public void GetSpace(InputAction.CallbackContext ctx)
    {
        //MinigameManager.Destroy(_manager.BurgerPlace.transform.GetChild(_manager.BurgerPlace.transform.childCount - 1).gameObject);
    }

    public void Update()
    {
        
    }
}
