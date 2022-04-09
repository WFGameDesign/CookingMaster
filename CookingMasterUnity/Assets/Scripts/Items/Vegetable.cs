using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : ItemBase
{
    //value denotes what type of vegetable this Gameobject is
    //0 = Lettuce
    //1 = Carrot
    //2 = Tomato
    //3 = Onion
    //4 = Radish
    //5 = Asparagus
    [SerializeField] private int veggieIndex;

    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getVegIndex()
    {
        return veggieIndex;
    }

    public override void destroyItem()
    {
        base.destroyItem();
    }
}
