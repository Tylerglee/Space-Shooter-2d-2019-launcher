﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadShotPowerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.QuadShotActive();
            }

            Destroy(this.gameObject);
        }
    }
}
