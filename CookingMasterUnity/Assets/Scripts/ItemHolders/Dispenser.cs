using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : ItemHolderBase
{

    [SerializeField] private float timeToDispenseNew;

    // Start is called before the first frame update
    void Start()
    {
        dispenseSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void itemRemovedNotify()
    {
        print(1);
        StartCoroutine(timedRespawn(timeToDispenseNew));
    }

    IEnumerator timedRespawn(float timer)
    {
        yield return new WaitForSeconds(timer);

        dispenseSpawn();
    }

    private void dispenseSpawn()
    {
        ItemBase newItem = Instantiate(Spawnable, placedItemAnchor.position, Quaternion.identity);
        newItem.transform.parent = placedItemAnchor;
        placeItem(newItem);
    }
}
