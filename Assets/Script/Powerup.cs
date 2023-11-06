using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField] //0 = Triple Shot 1 = Speed 2 = shields
    private int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3 (adjust in the inspector
        //When we leave the screen, destroy this object
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
           Destroy(this.gameObject);
        }

    }

    //Ontriggercollision
    //only be collectable by the player (Hint: Use Tags)
    //on collection destroy

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //communicate with the player script
            //handle to the component I want 
            //assign the handle to the component
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {                                  
                switch (powerupID) 
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        Debug.Log("Collected Speed Boost");
                        break;
                    case 2:
                        Debug.Log("Shield Collected");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }

}
