﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _quadShotPrefab;
    [SerializeField]
    private float _fireRate = 1.5f;
    [SerializeField]
    private float _canFire = -5.5f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;


    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false; 
    private bool _isShieldsActive = false;
    [SerializeField]
    private bool _isQuadShotActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

            
        if (_spawnManager == null )
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if(_uiManager == null )
        {
            Debug.LogError("The UI Manager is NULL.");        
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

       
            transform.Translate(direction * _speed * Time.deltaTime);
      
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {

            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);     
            
        }

        else if (_isQuadShotActive == true)
        {
           Instantiate(_quadShotPrefab, transform.position + new Vector3(-0.14f, .05f, 0), Quaternion.identity);
        }

        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }


        //if space key press,
        //if tripleshotactive is true
        //fire 3 lasers (triple shot prefab) 

        //instantiate 3 lasers (triple shot prefab)

    }

    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        

        _lives--;
       // _lives = _lives - 1;
       // _lives -= 1;


        if (_lives < 1)
        {
            //Communication with Spawn Manager
            //SpawnManager spawnManger = //Find the GameObject. Then get component
            //Let them know to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
        //start the power down coroutine for triple shot
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void QuadShotActive()
    {
        _isQuadShotActive = true;
        StartCoroutine(QuadShotPowerDownRoutine());
    }

    IEnumerator QuadShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isQuadShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f); 
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
