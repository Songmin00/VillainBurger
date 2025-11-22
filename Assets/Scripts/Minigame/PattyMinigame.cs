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
        _slider = manager.Slider.GetComponent<Slider>();
        _speed = manager.SliderSpeed;
    }

    public void GetInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isRunning = false;
            CheckResult();
        }        
    }

    public void Enter()
    {
        _manager.Slider.SetActive(true);
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
        }

        else
        {
            Debug.Log("실패");
            Restart();
        }
    }

    // 재시작 기능
    public void Restart()
    {
        isRunning = true;
    }
}
