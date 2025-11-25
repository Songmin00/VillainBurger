using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBun : DraggableImage
{
    [SerializeField] IngredientStat _topBunStat;
    [SerializeField] GameObject _topBunPrefab;

    public override void OnEndDrag(PointerEventData eventData)
    {
        // 부모, 위치 원복, 타겟이 맞다면 추가로 프리팹 생성
        _canvasGroup.alpha = 1f;//반투명 이미지 원복
        _canvasGroup.blocksRaycasts = true;//레이캐스트 블락 원복
        transform.SetParent(_originalParent);
        transform.localPosition = _originalTransform;

        if (IsDroppdeOnTarget(eventData, _target))
        {
            if (_target.transform.GetChild(0).childCount == 0)
            {
                _placeManager.PlaceIngredient(_stat, _ingredientPrefab);
            }
            else
            {
                _placeManager.PlaceIngredient(_topBunStat, _topBunPrefab);
            }            
        }        
    }    
}
