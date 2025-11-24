using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableVegetable : DraggableImage
{
    [SerializeField] protected GameObject _choppedPrefab;
    [SerializeField] protected IngredientPlace _burgerPlace;

    public override void OnEndDrag(PointerEventData eventData)
    {
        // 부모, 위치 원복, 타겟이 맞다면 추가로 프리팹 생성
        _canvasGroup.alpha = 1f;//반투명 이미지 원복
        _canvasGroup.blocksRaycasts = true;//레이캐스트 블락 원복
        transform.SetParent(_originalParent);
        transform.localPosition = _originalTransform;

        if (IsDroppdeOnTarget(eventData, _targetPlace))
        {
            SpawnIngredient();
            StartMinigame();
        }
        else if (IsDroppdeOnTarget(eventData, _burgerPlace))
        {
            SpawnChoppedOne();
            _myBurger.AddIngredient(_stat);
        }
    }

    private void SpawnChoppedOne()
    {
        GameObject ing = Instantiate(_choppedPrefab, _burgerPlace.transform.position, _burgerPlace.transform.rotation);
        ing.transform.SetParent(_burgerPlace.transform);
        _burgerPlace.GetComponent<BurgerPlace>().Sort();
    }
}
