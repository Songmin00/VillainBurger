using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _originalParent;
    private CanvasGroup _canvasGroup;
    private Vector3 _originalTransform;
    [SerializeField] GameObject _ingredientPrefab;
    [SerializeField] IngredientPlace _targetPannel;
    //[SerializeField] Transform _spawnPosition;

    private void Awake()
    {        
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        _originalTransform = transform.localPosition;
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        //1. 원래 부모 기억
        _originalParent = transform.parent;

        //2. 집을 나온다(드래그를 원활하게 하기 위해 최상위 계층으로 이동, 패널이 아닌 캔버스 하위로 들어가기)
        transform.SetParent(_originalParent.root);        

        //3. 드래깅 중인 이미지를 반투명하게 만듦(드래깅 중임을 표시)
        _canvasGroup.alpha = 0.5f;

        //4. 레이캐스트 블라킹을 끔(드롭 영역 감지가 원활하도록 만듦)
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(eventData.position);
        //드래깅(마우스 위치) 따라서 이동하기, eventData에는 위치정보 등 드래깅에 필요한 모든 정보가 포함
        transform.position = new Vector3(newPos.x, newPos.y, 0); 
            
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 부모, 위치 원복, 타겟이 맞다면 추가로 프리팹 생성
        _canvasGroup.alpha = 1f;//반투명 이미지 원복
        _canvasGroup.blocksRaycasts = true;//레이캐스트 블락 원복
        transform.SetParent(_originalParent);
        transform.localPosition = _originalTransform;

        if (IsDroppdeOnTarget(eventData) && !_targetPannel.IsPlaced)
        {
            SpawnIngredient();
        }             
    }

    //타겟에 드롭시켰는지 판단할 매서드
    public bool IsDroppdeOnTarget(PointerEventData eventData)
    {        
        if (eventData.pointerEnter == null)
        {
            return false;
        }
        return eventData.pointerEnter.transform.IsChildOf(_targetPannel.transform);
    }

    private void SpawnIngredient()
    {
        GameObject ing = Instantiate(_ingredientPrefab, _targetPannel.transform.position, _targetPannel.transform.rotation);
        ing.transform.SetParent(_targetPannel.transform);
        CheckPlacing();
    }

    public void CheckPlacing()
    {
        _targetPannel.GetComponent<IngredientPlace>().SwitchPlaceMode();
    }
}
