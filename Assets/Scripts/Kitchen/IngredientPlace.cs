using UnityEngine;

public class IngredientPlace : MonoBehaviour
{
    public MinigameManager _manager;
    [SerializeField] BurgerPlace _burgerPlace;

public virtual void PlayMinigame()
    {
        _manager.SetState(new IdleState(_manager));
    }

    public void TranslateImage()
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).position = _burgerPlace.CurrentPoint;
            transform.GetChild(0).parent = _burgerPlace.transform;
            _burgerPlace.Sort();
        }
    }

    public void RemoveImage()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        }
    }
}
