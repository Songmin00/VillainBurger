using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    [Header("패티 미니게임관련 필드")]
    [SerializeField] PattyMinigame _pattyMinigame;
    public GameObject Slider;
    public float SliderSpeed = 2f;

    private IMinigameState _currentState;

    private void Awake()
    {
        _currentState = new PattyMinigame(this);
    }
    private void Start()
    {
        
        SetState(new PattyMinigame(this));
    }

    private void Update()
    {
        _currentState.Update();
    }

    public void OnPressSpace(InputAction.CallbackContext ctx)
    {
        _currentState.GetInput(ctx);
    }

    public void SetState(IMinigameState minigameState)
    {
        _currentState?.Exit();
        _currentState = minigameState;
        _currentState.Enter();
    }
}
