using UnityEngine;

public class BurgerPlace : IngredientPlace
{
    public Vector2 CurrentPoint { get; private set; }
    private int _currentSort = 0;

    private void Awake()
    {
        CurrentPoint = transform.position;
    }

    public void Sort()
    {
        if (transform.childCount != 0)
        {
            transform.GetChild(transform.childCount - 1).GetComponent<SpriteRenderer>().sortingOrder = _currentSort;
            transform.GetChild(transform.childCount - 1).position = CurrentPoint;
            CurrentPoint = transform.GetChild(transform.childCount - 1).GetChild(0).position;
            _currentSort++;
        }
        else
        {
            _currentSort = 0;
            CurrentPoint = transform.position;
        }
    }

    public override void PlayMinigame()
    {
        _manager.SetState(new ToppingMinigame(_manager));
    }
}
