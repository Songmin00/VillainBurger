using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableVegetable : DraggableImage
{
    [SerializeField] protected IngredientStat _rawStat;
    [SerializeField] protected GameObject _rawPrefab;
    [SerializeField] protected Transform _targetInRaw;


    public override void OnEndDrag(PointerEventData eventData)
    {
        // 부모, 위치 원복, 타겟이 맞다면 추가로 프리팹 생성
        _canvasGroup.alpha = 1f;//반투명 이미지 원복
        _canvasGroup.blocksRaycasts = true;//레이캐스트 블락 원복
        transform.SetParent(_originalParent);
        transform.localPosition = _originalTransform;

        if (IsDroppdeOnTarget(eventData, _target))
        {
            _manager.PlaceIngredient(_stat, _ingredientPrefab);
        }
        else if (IsDroppdeOnTarget(eventData, _targetInRaw))
        {
            _manager.PrepareVegetable(_rawStat, _rawPrefab);
        }
    }   
}
