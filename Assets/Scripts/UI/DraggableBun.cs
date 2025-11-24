using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBun : DraggableImage
{
    [SerializeField] GameObject _topBunPrefab;
    public override void OnEndDrag(PointerEventData eventData)
    {
        // 부모, 위치 원복, 타겟이 맞다면 추가로 프리팹 생성
        _canvasGroup.alpha = 1f;//반투명 이미지 원복
        _canvasGroup.blocksRaycasts = true;//레이캐스트 블락 원복
        transform.SetParent(_originalParent);
        transform.localPosition = _originalTransform;

        if (IsDroppdeOnTarget(eventData, _targetPlace))
        {
            if (_targetPlace.transform.childCount == 0)
            {
                SpawnIngredient();
                _targetPlace.GetComponent<BurgerPlace>().Sort();
            }
            else
            {
                SpawnTopBun();
                _targetPlace.GetComponent<BurgerPlace>().Sort();
            }            
        }
        
    }

    private void SpawnTopBun()
    {
        GameObject ing = Instantiate(_topBunPrefab, _targetPlace.transform.position, _targetPlace.transform.rotation);
        ing.transform.SetParent(_targetPlace.transform);        
    }
    
}
