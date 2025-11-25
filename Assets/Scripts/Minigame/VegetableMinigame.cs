using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VegetableMinigame : IMinigameState
{
    MinigameManager _manager;
    private Slider _slider;
    private float _firstPoint;
    private float _minusSpeed;
    private float _plusValue;
    private bool isRunning = true;


    public VegetableMinigame(MinigameManager manager)
    {
        _manager = manager;        
        _slider = manager.VegetableSlider.GetComponent<Slider>();
        _minusSpeed = manager.MinusSpeed;
        _plusValue = manager.PlusValue;
        _firstPoint = manager.FirstPoint;
    }

    public void GetSpace(InputAction.CallbackContext ctx)
    {        
        if (isRunning)
        {        
            _slider.value += _plusValue;
        }
    }

    public void Enter()
    {
        _manager.VegetableSlider.SetActive(true);
        _slider.value = _firstPoint;
    }

    public void Update()
    {
        float pos = _slider.value;

        if (pos > 0.85f)
        {
            Debug.Log("성공!");
            isRunning = false;
            _manager.PlaceManager.WinVegetableGame();
        }
        else if (isRunning)
        {
            // 슬라이더를 지속적 후퇴
            _slider.value -= Time.deltaTime * _minusSpeed;
        }
    }

    public void Exit()
    {

    }

    //쓰지 않는 매서드
    public void GetUpArrow(InputAction.CallbackContext ctx)
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
}
