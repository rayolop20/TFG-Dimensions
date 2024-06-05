using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMovment player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovment>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.coinNumber++;
            gameObject.SetActive(false);
        }
    }
}
