using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    //references to power up gameobject prefabs
    [SerializeField] private GameObject[] PowerUpArray;

    //reference to central spawn point
    [SerializeField] private Transform spawnAnchor;

    //distance in x and z directions that powerup can randomly spawn
    [SerializeField] private float distVariance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnPowerUp( CharacterInfo playerIndex)
    {
        //randomize spawn point from a center anchor
        Vector3 spawnPoint = new Vector3(spawnAnchor.position.x + Random.Range(-distVariance, distVariance), spawnAnchor.position.y, spawnAnchor.position.z + Random.Range(-distVariance, distVariance));

        //spawn gameobject
        GameObject spawnHolder = Instantiate(PowerUpArray[Random.Range(0, PowerUpArray.Length)], spawnPoint, Quaternion.identity);

        //get power up base
        PowerUpBase pUHolder = spawnHolder.GetComponent<PowerUpBase>();

        if(pUHolder != null)
        {
            pUHolder.setIntendedPlayer(playerIndex);
        }
    }
}
