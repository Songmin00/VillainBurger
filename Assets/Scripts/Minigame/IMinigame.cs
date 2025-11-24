using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public interface IMinigameState
{
    public void GetSpace(InputAction.CallbackContext ctx);
    public void GetUpArrow(InputAction.CallbackContext ctx);
    public void GetDownArrow(InputAction.CallbackContext ctx);
    public void GetLeftArrow(InputAction.CallbackContext ctx);
    public void GetRightArrow(InputAction.CallbackContext ctx);
    public void Enter();
    public void Update();
    public void Exit();    
}
