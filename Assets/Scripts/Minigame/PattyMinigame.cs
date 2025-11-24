using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PattyMinigame : IMinigameState
{
    MinigameManager _manager;
    private Slider _slider;
    private float _speed;
    private bool isRunning = true;    


    public PattyMinigame(MinigameManager manager)
    {
        _manager = manager;        
        _slider = manager.PattySlider.GetComponent<Slider>();
        _speed = manager.SlideSpeed;
    }

    public void GetSpace(InputAction.CallbackContext ctx)
    {
       isRunning = false;
       CheckResult();               
    }

    public void Enter()
    {
        _manager.PattySlider.SetActive(true);
        _slider.value = 0;
    }

    public void Update()
    {
        if (isRunning)
        {
            // 슬라이더를 0~1까지 왕복시키기
            _slider.value = Mathf.PingPong(Time.time * _speed, 1f);
        }
    }

    public void Exit()
    {
        
    }

    private void CheckResult()
    {
        float pos = _slider.value;

        // 성공 판정
        if (pos > 0.45f && pos < 0.6f)
        {
            Debug.Log("성공!");
            _manager.SetState(new IdleState(_manager));
            _manager.PattyPlace.TranslateImage();
        }

        else
        {
            Debug.Log("실패");
            _manager.SetState(new IdleState(_manager));
            _manager.PattyPlace.RemoveImage();
            _manager.MyBurger.RemoveIngredient();
            
            //Restart();
        }
    }

    // 재시작 기능
    public void Restart()
    {
        isRunning = true;
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
