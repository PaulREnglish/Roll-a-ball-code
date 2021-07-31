using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    public GameObject PlayerPrefab;
    
    int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(PlayerController.GetPlayer().activeInHierarchy == true)
        {
            transform.position = PlayerController.PlayerPositions[i];
        }
        i++;
    }
}
