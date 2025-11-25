using System.Collections;
using UnityEngine;


//미니게임 매니저의 상태 전환도 여기서 해야 함(각 미니게임에는 없게 할 것).
public class PlaceManager : MonoBehaviour
{
    [Header("매니저")]
    [SerializeField] MinigameManager _minigameManager;
    [SerializeField] IngredientPoolManager _poolManager;

    [Header("재료 배치 장소")]
    [SerializeField] Transform _pattyPlace;
    [SerializeField] Transform _burgerPlace;
    [SerializeField] Transform _vegetablePlace;

    [Header("MyBurger")]
    [SerializeField] MyBurger _myburger;


    private Vector2 _burgerPoint;
    private IngredientStat _currentStat;
    private int _currentSort;
    private GameObject _currentPrefab;
    private GameObject _currentInstance;



    private void Awake()
    {
        _burgerPoint = _burgerPlace.position;
        _currentSort = 0;
    }


    #region DraggableImage에서 호출
    //소스, 토핑, 손질채소, 빵 만들기 용 호출.
    public void OnDroppedBurgerPlace(GameObject prefab)
    {
        
        IPoolable ing = MakeIngredient(prefab);
        GameObject newIng = (ing as MonoBehaviour).gameObject;
        newIng.transform.SetParent(_myburger.transform);
        newIng.transform.position = _burgerPoint;
        newIng.GetComponent<Renderer>().sortingOrder = ++_currentSort;
        _burgerPoint = newIng.gameObject.transform.GetChild(0).transform.position;
        switch (prefab.GetComponent<Ingredient>().Stat.Type)
        {
            case IngredientType.Patty:
                Debug.Log("패티는 그릴로.");
                break;
            case IngredientType.Sauce:
                break;
            case IngredientType.Topping:
                _minigameManager.SetState(new ToppingMinigame(_minigameManager));
                break;
            case IngredientType.Vegetable:
                break;
            case IngredientType.Bun:
                if (_myburger.transform.childCount > 0)
                {
                    //버거 제출 매서드
                }
                break;
        }
    }

    //그릴에 패티 배치
    public void OnDroppedPattyPlace(GameObject patty)
    {
        IPoolable ing = MakeIngredient(patty);
        (ing as MonoBehaviour).transform.SetParent(_pattyPlace);
        (ing as MonoBehaviour).transform.position = _pattyPlace.position;
        _minigameManager.SetState(new PattyMinigame(_minigameManager));
    }

    //도마에 생채소 배치
    public void OnDroppedVegetablePlace(GameObject rawVegetable)
    {
        IPoolable ing = MakeIngredient(rawVegetable);
        (ing as MonoBehaviour).transform.position = _vegetablePlace.position;
        _minigameManager.SetState(new VegetableMinigame(_minigameManager));
    }
    #endregion

    #region 미니게임에서 호출
    //패티 미니게임 성공 시
    public void WinPattyGame()
    {
        Transform patty = _pattyPlace.GetChild(0);
        patty.SetParent(_myburger.transform);
        StartCoroutine(SlidePatty(patty));
        patty.GetComponent<Renderer>().sortingOrder = ++_currentSort;
        _burgerPoint = patty.transform.position;
        _minigameManager.SetState(new IdleState(_minigameManager));
    }

    //채소 미니게임 성공시
    public void WinVegetableGame()
    {
        Transform vegetable = _vegetablePlace.GetChild(0);
        RemoveIngredientInPlace(_vegetablePlace);
        //재고 갱신
        _minigameManager.SetState(new IdleState(_minigameManager));
    }

    //토핑 미니게임 성공시
    public void WinToppingGame()
    {
        _minigameManager.SetState(new IdleState(_minigameManager));
    }

    //패티 미니게임 실패시
    public void FailPattyGame()
    {
        Transform patty = _pattyPlace.GetChild(0);
        RemoveIngredientInPlace(patty);
    }

    //토핑 미니게임 실패시
    public void FailToppingGame()
    {
        Transform topping = _myburger.transform.GetChild(_myburger.transform.childCount - 1);
        RemoveIngredientInPlace(topping);
        _burgerPoint = _myburger.transform.GetChild(_myburger.transform.childCount - 1).position;
    }
    #endregion






    //지역함수
    private IPoolable MakeIngredient(GameObject prefab)
    {
        return _poolManager.Get(prefab);
    }

    private void RemoveIngredientInPlace(Transform place)
    {
        _poolManager.Return(place.GetChild(place.childCount - 1).gameObject);
    }
   
    private IEnumerator SlidePatty(Transform patty)
    {
        Vector3 startPos = patty.localPosition;
        Vector3 endPos = _burgerPoint;
        float current = 0;

        while (current < 1)
        {
            current += Time.deltaTime * 5f;
            patty.position = Vector3.Lerp(startPos, endPos, current);
            yield return null;
        }
    }
    
}
