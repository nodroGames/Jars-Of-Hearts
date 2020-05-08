using System.Collections;
using UnityEngine;

public class RotTimer : MonoBehaviour
{
    //[SerializeField]
    //private ItemDatabaseObject database;
    [SerializeField]
    private RotProduct rotProduct;
    [SerializeField]
    private float rotBaseTime;
    [SerializeField]
    private Player player = default;

    private Item item;
    private float currentRotRate;
    private float currentRotTime;

    private IEnumerator timerCoroutine;

    void Start()
    {
        player.OnAdded += StartRot;
    }

    private void Update()
    {

    }

    public void StartRot(Item _item, float _rotTime, float _rotRate)
    {
        item = _item;

        currentRotTime = _rotTime;
        currentRotRate = _rotRate;
        timerCoroutine = RunCountdown(item, currentRotRate, currentRotRate);
        StartCoroutine(timerCoroutine);
    }

    private IEnumerator RunCountdown(Item _item, float _currentRotTime, float _currentRotRate)
    {
        Debug.Log("Before " + _currentRotTime);
        yield return new WaitForSeconds(_currentRotTime -= (Time.deltaTime * _currentRotRate));
        //yield return new WaitForSeconds(_currentRotTime -= (_currentRotRate));
        Debug.Log("After " + _currentRotTime);
        if (rotProduct.SetupProduct(_item, _currentRotTime, _currentRotRate))
        {
        }
        yield return null;
    }

    public void OnDestroy()
    {
        player.OnAdded -= StartRot;
    }
}
