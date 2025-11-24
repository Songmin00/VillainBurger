using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTopping : DraggableImage
{
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
            _targetPlace.GetComponent<BurgerPlace>().Sort();
            _myBurger.AddIngredient(_stat);
            StartMinigame();
        }
    }
}
