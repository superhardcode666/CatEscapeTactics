﻿using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class LevelGenerator : MonoBehaviour
{
    public GameObject StartTile;

    [SerializeField] private GameObject[] _tiles;
    [SerializeField] private float _currentLevelSpeed = 4;
    [SerializeField] private float _speedFactor = .5f;

    [Range(0f, 1f)] public float _catNipProbability = 0.2f; 
    
    private float Index = 0;
    
    private void Start()
    {
        SpawnStartTiles();
    }

    private void SpawnStartTiles()
    {
        float[] xPositions = { 7f, -1f, -9f, -17f, -25f };

        for (int i = 0; i < xPositions.Length; i++)
        {
            GameObject startPlane = Instantiate(StartTile, transform);
            startPlane.transform.position = new Vector3(xPositions[i], 0, 0);
        }
    }
    
    private void Update()
    {
        gameObject.transform.position += new Vector3(_currentLevelSpeed * Time.deltaTime, 0, 0);

        if(transform.position.x >= Index)
        {
            SpawnTile(_tiles[Random.Range(0, _tiles.Length)], new Vector3(-16, 0, 0));
            SpawnTile(_tiles[Random.Range(0, _tiles.Length)], new Vector3(-24, 0, 0));
            
            Index += 15.95f;
            _currentLevelSpeed += _speedFactor;
        }
    }

    private void SpawnTile(GameObject tile, Vector3 tilePosition)
    {
        GameObject tempTile = Instantiate(tile, transform);
        tempTile.transform.position = tilePosition;
        
        if (Random.value <= _catNipProbability)
        {
            LevelTile currentTile = tempTile.GetComponent<LevelTile>();
            SpawnCatNip(currentTile);
        }
    }

    public void SetLevelSpeed(float newSpeed)
    {
        _currentLevelSpeed = newSpeed;
    }
    
    public float GetLevelSpeed()
    {
        return _currentLevelSpeed;
    }
    
    private void SpawnCatNip(LevelTile tile)
    {
        tile.SpawnCatNip();
    }
}
