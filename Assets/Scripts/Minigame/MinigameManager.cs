using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public PlaceManager PlaceManager;

    [Header("패티 미니게임 필드")]
    public GameObject PattySlider;
    public float SlideSpeed = 2f;
     
    [Header("채소 미니게임 필드")]
    public GameObject VegetableSlider;
    public float FirstPoint = 0.3f;
    public float MinusSpeed = 1f;
    public float PlusValue = 0.15f;

    [Header("토핑 미니게임 필드")]
    public Transform CommandPannel;    
    public Sprite UpArrowImage;
    public Sprite DownArrowImage;
    public Sprite LeftArrowImage;
    public Sprite RightArrowImage;
    public int CommandCount = 8;

    private IMinigameState _currentState;    
    
    private void Start()
    {
        SetState(new IdleState(this));
        //SetState(new PattyMinigame(this));
        //SetState(new VegetableMinigame(this));
        //SetState(new ToppingMinigame(this));
    }

    private void Update()
    {
        _currentState?.Update();
    }

    public void SetState(IMinigameState minigameState)
    {
        _currentState?.Exit();
        _currentState = minigameState;
        _currentState.Enter();
    }

    public void OnPressSpace(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _currentState.GetSpace(ctx);
        }        
    }
    public void OnPressUpArrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _currentState.GetUpArrow(ctx);
        }            
    }
    public void OnPressDownArrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _currentState.GetDownArrow(ctx);
        }            
    }
    public void OnPressLeftArrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _currentState.GetLeftArrow(ctx);
        }        
    }
    public void OnPressRightArrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _currentState.GetRightArrow(ctx);
        }        
    }


}
