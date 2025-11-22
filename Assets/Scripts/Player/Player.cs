using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private int _money;
    private int _essence;
    private int _level;
    [SerializeField] private MinigameManager _minigameManager;
        
        
    

    public void PlayPatty()
    {
        _minigameManager.SetState(new PattyMinigame(_minigameManager));
    }
}
