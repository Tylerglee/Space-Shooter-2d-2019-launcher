﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
               
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7.5f, 0);      
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Hit: " + other.transform.name);

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
           // other.transform.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
            
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
