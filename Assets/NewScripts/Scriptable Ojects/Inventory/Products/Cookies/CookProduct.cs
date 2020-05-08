using System.Collections;
using UnityEngine;

public class CookProduct : MonoBehaviour
{
    //[SerializeField]
    //private ItemDatabaseObject database;
    [SerializeField]
    private CookingStates cookingStates;
    [SerializeField]
    private float cookTime;

    private Item item;

    private IEnumerator timerCoroutine;

    public void StartTimer(Item _item)
    {
        item = _item;
        timerCoroutine = RunCountdown(item);
        StartCoroutine(timerCoroutine);
    }

    private IEnumerator RunCountdown(Item item)
    {
        yield return new WaitForSeconds(cookTime);

        if (cookingStates.SetupState(item))
        {
        }
        yield return null;
    }
}
