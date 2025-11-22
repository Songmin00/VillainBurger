using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public interface IMinigameState
{
    public void GetInput(InputAction.CallbackContext ctx);
    public void Enter();
    public void Update();
    public void Exit();    
}
