using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ToppingMinigame : IMinigameState
{
    MinigameManager _manager;
    private Queue<int> _randomCommand;
    private int _currentPlayerCommand = 10;
    private int _currentRandomCommand = 0;
    private Transform _commandPannel;    
    private Sprite _upArrow;
    private Sprite _downArrow;
    private Sprite _leftArrow;
    private Sprite _rightArrow;
    private int _commandCount;

    public ToppingMinigame(MinigameManager manager)
    {        
        _manager = manager;
        _commandPannel = manager.CommandPannel;
        _upArrow = manager.UpArrowImage;
        _downArrow = manager.DownArrowImage;    
        _leftArrow = manager.LeftArrowImage;
        _rightArrow = manager.RightArrowImage;
        _commandCount = manager.CommandCount;

        _manager.CommandPannel.parent.gameObject.SetActive(true);

        _randomCommand = new Queue<int>();
        for (int i = 0; i < _commandCount; i++)
        {
            int random = Random.Range(0, 4);
            _randomCommand.Enqueue(random);
            SetCommandImage(random, i);
        }
    }

    public void Enter()
    {
        
    }
    public void Update()
    {
        if (_randomCommand.Count == 0)
        {
            Debug.Log("성공!");
            _manager.SetState(new IdleState(_manager));
        }
    }

    public void Exit()
    {
        
    }

    public void SetCommandImage(int value, int index)
    {
        Image image = _commandPannel.transform.GetChild(index).GetComponent<Image>();
        image.GetComponent<CanvasGroup>().alpha = 0.5f;
        switch (value)
        {
            case 0:               
                image.sprite = _upArrow;                
                break;
            case 1:                
                image.sprite = _downArrow;
                break;
            case 2:                
                image.sprite = _leftArrow;
                break;
            case 3:                
                image.sprite = _rightArrow;
                break;
        }
    }

    

    public void CheckCommand()
    {
        if (_randomCommand.Dequeue() == _currentPlayerCommand)
        {
            _commandPannel.transform.GetChild(_currentRandomCommand).GetComponent<CanvasGroup>().alpha = 1f;
            _currentRandomCommand += 1;
        }
        else
        {
            Debug.Log("실패");
            _manager.BurgerPlace.RemoveImage();
            _manager.MyBurger.RemoveIngredient();
            _manager.SetState(new IdleState(_manager));
        }
    }


    
    public void GetUpArrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _currentPlayerCommand = 0;
            Debug.Log(_currentPlayerCommand);
            CheckCommand();
        }        
        
    }
    public void GetDownArrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _currentPlayerCommand = 1;
            Debug.Log(_currentPlayerCommand);
            CheckCommand();
        }
    }

    public void GetLeftArrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _currentPlayerCommand = 2;
            Debug.Log(_currentPlayerCommand);
            CheckCommand();
        }
    }

    public void GetRightArrow(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _currentPlayerCommand = 3;
            Debug.Log(_currentPlayerCommand);
            CheckCommand();
        }
    }

    //안쓰는 매서드
    public void GetSpace(InputAction.CallbackContext ctx)
    {

    }
}
