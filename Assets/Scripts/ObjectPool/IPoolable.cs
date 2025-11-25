using UnityEngine;

public interface IPoolable
{
    void OnCreate(Transform parent);
    void OnGet();
    void OnReturn(Transform parent);
}
