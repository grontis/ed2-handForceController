using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWinSpace : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float winRadius = 4;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < winRadius)
        {
            GetComponent<WinHandler>().HandleWin();
        }
    }
}
