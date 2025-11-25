using System.Collections;
using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    [SerializeField] MyBurger _myburger;
    [SerializeField] MinigameManager _minigameManager;
    [SerializeField] PoolManager _poolManager;
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
        _currentStat = stat;
        AddInMyBurger(stat);
        GetStatAndPrefab(stat, prefab);
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
    public void PrepareVegetable(IngredientStat stat, GameObject prefab)
    {
        GetStatAndPrefab(_currentStat, prefab);
        _currentInstance = _poolManager.GetIngredient(_currentStat, _currentPrefab);
        _currentInstance.transform.position = _vegetablePlace.position;
        _currentInstance.transform.SetParent(_vegetablePlace.transform);
        _minigameManager.SetState(new VegetableMinigame(_minigameManager));
    }

    // 패티를 그릴로 배치할 땐 여기 호출
    public void PlacePattyInGrill(IngredientStat stat, GameObject prefab)
    {
        GetStatAndPrefab(stat, prefab);
        _currentInstance = _poolManager.GetIngredient(_currentStat, _currentPrefab);
        _currentInstance.transform.position = _pattyPlace.position;
        _currentInstance.transform.SetParent(_pattyPlace.transform);
        _minigameManager.SetState(new PattyMinigame(_minigameManager));
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
        SpawnInMyBurger();
        SetBurgerPoint();
        SetPrefabSort();
        _minigameManager.SetState(new ToppingMinigame(_minigameManager));
    }

    private void MakeSauce()
    {
        SpawnInMyBurger();
        SetBurgerPoint();
        SetPrefabSort();        
    }

    private void MakeVegetable()
    {
        SpawnInMyBurger();
        SetBurgerPoint();
        SetPrefabSort();
    }

    private void MakeBun()
    {
        Transform myBurger = _burgerPlace.GetChild(0);
        SpawnInMyBurger();
        SetBurgerPoint();
        SetPrefabSort();
        if (myBurger.childCount > 0)
        {
            //버거 제출 매서드 호출 예정
        }
    }

    #endregion


    #region 재료 게임오브젝트 생성, 이동 및 파괴용 지역함수
    private void GetStatAndPrefab(IngredientStat stat, GameObject prefab)
    {
        _currentStat = stat;
        _currentPrefab = prefab;        
    }

    private void SpawnInMyBurger()
    {
        Transform myBurger = _burgerPlace.GetChild(0);
        _currentInstance = _poolManager.GetIngredient(_currentStat, _currentPrefab);
        _currentInstance.transform.position = _burgerPoint;
        _currentInstance.transform.SetParent(myBurger);
    }
    private void ReplaceToBurger()
    {
        Transform myBurger = _burgerPlace.GetChild(0);
        GameObject patty = _currentInstance;
        patty.transform.SetParent(_burgerPlace);
        StartCoroutine(SlideToBurger(patty));
    }

    private IEnumerator SlideToBurger(GameObject ing)
    {
        float t = 0f;
        Vector3 startPos = ing.transform.position;
        Vector3 targetPos = _burgerPoint;
        while(t < 1f)
        {
            t += Time.deltaTime * 5f;
            ing.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
    }

    private void RemoveIngredient()
    {
        _poolManager.ReturnIngredient(_currentInstance);
        _currentInstance.transform.SetParent(_poolManager.transform);
    }

    #endregion


    #region 재료 배치 후 배치 장소 및 레이어 조정용 하위 지역함수
    private void SetBurgerPoint()
    {
        Transform myBurger = _burgerPlace.GetChild(0);
        if (myBurger.childCount > 0)
        {
            _burgerPoint = myBurger.transform.GetChild(myBurger.childCount - 1).GetChild(0).position;
        }
        else
        {
            _burgerPoint = myBurger.transform.position;
        }
    }
    private void SetPrefabSort()
    {
        _currentInstance.GetComponent<Renderer>().sortingOrder = _currentSort;
        _currentSort++;
    }
    #endregion


    #region 비교용 MyBurger에 스탯 정보를 추가 및 삭제하기 위한 지역함수
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
