using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


//소스, 토핑은 여기로 사용
//패티, 채소, 빵은 각자 상속 클래스로.
public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    protected Transform _originalParent;
    protected CanvasGroup _canvasGroup;
    protected Vector3 _originalTransform;
    
    [SerializeField] protected PlaceManager _placeManager;
    [SerializeField] protected GameObject _ingredientPrefab;
    [SerializeField] protected Transform _target;
    [SerializeField] protected IngredientStat _stat;
    
    

    private void Awake()
    {        
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        _originalTransform = transform.localPosition;
        
            _placeManager = FindFirstObjectByType<PlaceManager>();
        
        
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        //1. 원래 부모 기억
        _originalParent = transform.parent;

        //2. 집을 나온다(드래그를 원활하게 하기 위해 최상위 계층으로 이동, 패널이 아닌 캔버스 하위로 들어가기)
        transform.SetParent(_originalParent.root);        

        //3. 드래깅 중인 이미지를 반투명하게 만듦(드래깅 중임을 표시)
        _canvasGroup.alpha = 0.6f;

        //4. 레이캐스트 블라킹을 끔(드롭 영역 감지가 원활하도록 만듦)
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(eventData.position);
        //드래깅(마우스 위치) 따라서 이동하기, eventData에는 위치정보 등 드래깅에 필요한 모든 정보가 포함
        transform.position = new Vector3(newPos.x, newPos.y, 0); 
            
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        // 부모, 위치 원복, 타겟이 맞다면 추가로 프리팹 생성
        _canvasGroup.alpha = 1f;//반투명 이미지 원복
        _canvasGroup.blocksRaycasts = true;//레이캐스트 블락 원복
        transform.SetParent(_originalParent);
        transform.localPosition = _originalTransform;

        if (IsDroppdeOnTarget(eventData, _target))
        {
            _placeManager.PlaceIngredient(_stat, _ingredientPrefab);
        }
    }

    //타겟에 드롭시켰는지 판단할 매서드
    public bool IsDroppdeOnTarget(PointerEventData eventData, Transform target)
    {        
        if (eventData.pointerEnter == null)
        {
            return false;
        }
        return eventData.pointerEnter.transform.IsChildOf(target.transform);
    }   
}
