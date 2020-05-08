using UnityEngine;

public class RotProduct : MonoBehaviour
{

    [SerializeField]
    private float rotBaseTime;
    [SerializeField]
    private InventoryType healthyHeart;
    [SerializeField]
    private InventoryType quarterHeart;
    [SerializeField]
    private InventoryType halfHeart;
    [SerializeField]
    private InventoryType mush;
    [SerializeField]
    private RotTimer rotTimer;

    private InventoryType currentProduct;
    private Item item;
    private float currentRotTime = 0f;
    private float currentRotRate = 0f;
    private float half = .50f;
    private float quarter = .75f;

    public bool SetupProduct(Item _item, float _currentRotTime, float _currentRotRate)
    {
        if (_currentRotTime <= rotBaseTime * 0)
        {
            Debug.Log("Mush State");
            currentProduct = mush;
            //disable animator
            //update inventory
            return false;
        }
        else
        {
            Debug.Log("else is getting called");
            item = _item;
            currentRotTime = _currentRotTime;
            currentRotRate = _currentRotRate;

            RotCurrentProduct();
            return true;
        }
    }

    private void RotCurrentProduct()
    {
        if (item.Id >= 0)
        {
            // Changes to half rot state
            if (currentRotTime <= (rotBaseTime * half))
            {
                Debug.Log("This should get to the half state");
                //check animator
                //if (obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled == false)
                //{
                //    obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled = true;
                //}
                if (currentRotTime > 0.00f)
                {
                    currentRotTime -= Time.deltaTime * currentRotRate;
                }
                else { return; }

                currentProduct = halfHeart;
                //update inventory
            }

            // Changes to quarter rot state
            else if (currentRotTime <= (rotBaseTime * quarter))
            {
                Debug.Log("This should get to the quarter state");
                //check animator
                //if (obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled == false)
                //{
                //    obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled = true;
                //}
                if (currentRotTime > 0.00f)
                {
                    //currentRotTime -= Time.deltaTime * currentRotRate;
                }

                else { return; }
                currentProduct = quarterHeart;
                //update inventory
            }

            // Starts at healthy heart state
            else
            {
                Debug.Log("This should get to the healthy state");
                //check animator
                //if (obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled == false)
                //{
                //    obj.Value.slotDisplay.GetComponentInChildren<Animator>().enabled = true;
                //}
                if (currentRotTime > 0.00f)
                {
                    rotTimer.StartRot(item, currentRotTime, currentRotRate);
                }
                else { return; }
            }
        }
    }
}
