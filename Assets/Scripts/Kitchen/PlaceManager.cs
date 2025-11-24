using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    [SerializeField] MyBurger _myburger;
    [SerializeField] MinigameManager _manager;
    [SerializeField] Transform _pattyPlace;
    [SerializeField] Transform _burgerPlace;
    [SerializeField] Transform _vegetablePlace;
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

    #region 드래그 앤 드롭에서 호출하는 매서드

    // 버거판에 배치할 땐 여기 호출
    public void PlaceIngredient(IngredientStat stat, GameObject prefab)
    {
        AddInMyBurger(stat);
        GetPrefab(prefab);
        switch (stat.Type)
        {            
            case IngredientType.Vegetable:
                MakeVegetable();
                break;

            case IngredientType.Topping:
                MakeTopping();
                break;

            case IngredientType.Sauce:
                MakeSauce();
                break;

            case IngredientType.Bun:
                MakeBun();
                break;

            default:
                Debug.Log("패티는 그릴로.");
                break;
        }
    }

    // 채소를 도마로 배치할 땐 여기 호출
    public void PrepareVegetable(GameObject prefab)
    {
        GetPrefab(prefab);
        _currentInstance = Instantiate(_currentPrefab, _vegetablePlace.position, _vegetablePlace.rotation);
        _currentInstance.transform.SetParent(_vegetablePlace.transform);
        _manager.SetState(new VegetableMinigame(_manager));
    }

    // 패티를 그릴로 배치할 땐 여기 호출
    public void PlacePattyInGrill(IngredientStat stat, GameObject prefab)
    {
        GetPrefab(prefab);
        _currentInstance = Instantiate(_currentPrefab, _pattyPlace.position, _pattyPlace.rotation);
        _currentInstance.transform.SetParent(_pattyPlace.transform);
        _manager.SetState(new PattyMinigame(_manager));
    }
    #endregion






    #region 미니게임 매니저에서 호출하는 매서드
    //채소 미니게임 성공 시 호출
    public void WinVegetableMinigame()
    {
        RemoveIngredient();
    }

    //패티 미니게임 성공시 호출
    public void WinPattyMinigame()
    {
        AddInMyBurger(_currentStat);
        ReplaceToBurger();
        SetBurgerPoint();
        SetPrefabSort();
    }

    //패티 미니게임 실패 시 호출
    public void FailPattyMinigame()
    {
        RemoveIngredient();
    }

    //토핑 미니게임 실패 시 호출
    public void FailToppingMinigame()
    {
        RemoveInMyBurger();
        RemoveIngredient();
        SetBurgerPoint();
    }
    #endregion







    //이 아래부터는 private 지역함수
    #region 종류별로 묶은 재료 배치용 지역함수
    private void MakeTopping()
    {
        SpawnInBurgerPlace();
        SetBurgerPoint();
        SetPrefabSort();
        _manager.SetState(new ToppingMinigame(_manager));
    }

    private void MakeSauce()
    {
        SpawnInBurgerPlace();
        SetBurgerPoint();
        SetPrefabSort();        
    }

    private void MakeVegetable()
    {
        SpawnInBurgerPlace();
        SetBurgerPoint();
        SetPrefabSort();
    }

    private void MakeBun()
    {
        SpawnInBurgerPlace();
        SetBurgerPoint();
        SetPrefabSort();
        if (_burgerPlace.childCount > 0)
        {
            //버거 제출 매서드 호출 예정
        }
    }

    #endregion


    #region 재료 게임오브젝트 생성, 이동 및 파괴용 지역함수
    private void GetPrefab(GameObject prefab)
    {
        _currentPrefab = prefab;        
    }

    private void SpawnInBurgerPlace()
    {
        _currentInstance = Instantiate(_currentPrefab, _burgerPoint, _burgerPlace.rotation);
        _currentInstance.transform.SetParent(_burgerPlace.transform);
    }
    private void ReplaceToBurger()
    {
        _currentInstance.transform.SetParent(_burgerPlace.parent);
        _currentInstance.transform.position = _burgerPoint;
    }

    private void RemoveIngredient()
    {
        Destroy(_currentInstance.gameObject);
    }

    #endregion


    #region 재료 배치 후 배치 장소 및 레이어 조정용 하위 지역함수
    private void SetBurgerPoint()
    {
        _burgerPoint = _burgerPlace.transform.GetChild(_burgerPlace.childCount - 1).GetChild(0).position;
    }
    private void SetPrefabSort()
    {
        _currentInstance.GetComponent<Renderer>().sortingOrder = _currentSort;
        _currentSort++;
    }
    #endregion


    #region 비교용 MyBurger에 추가 및 삭제하기 위한 지역함수
    private void AddInMyBurger(IngredientStat stat)
    {
        _currentStat = stat;
        _myburger.AddIngredient(_currentStat);
    }

    private void RemoveInMyBurger()
    {
        _myburger.RemoveIngredient();
    }
    #endregion
}
