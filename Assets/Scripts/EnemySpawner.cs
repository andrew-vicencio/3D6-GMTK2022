using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] SpawnPoints;

    public float TimeBetweenWave = 5;

    public int WaveStrength = 8;

    private int TempWaveStrength;

    public float TimeBetweenSpawns = 1;

    public float SpawnTimeVariance = 0.3f;

    public float SpawnXVariance = 5f;

    public float SpawnZVariance = 5f;

    public int CurrentWave = 0;

    public int MaxWave = 5;

    private bool WaveActive = false;

    private float tempTime;

    public GameObject[] Enemy;
    
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        tempTime = 0;
    }

    public void Lost(){
    }

    // Update is called once per frame
    void Update()
    {

        if(manager.running){
            if(WaveActive){
                tempTime -= Time.deltaTime;
                if(tempTime <= 0){
                    if(TempWaveStrength > 0){
                        int index = Random.Range(0, SpawnPoints.Length);
                        var tempX = SpawnPoints[index].transform.position.x + Random.Range(-SpawnXVariance,SpawnXVariance);
                        var tempZ = SpawnPoints[index].transform.position.z + Random.Range(-SpawnZVariance,SpawnZVariance);
                        var spawnLoc = new Vector3(tempX,SpawnPoints[index].transform.position.y,tempZ);
                        GameObject enemy = Instantiate(Enemy[Random.Range(0,Enemy.Length)], spawnLoc,Quaternion.identity,transform);
                        TempWaveStrength -= 1;
                    }
                    if(TempWaveStrength <= 0){
                        TimeBetweenWave += 0.5f;
                        WaveStrength += 2;
                        WaveActive = false;
                        tempTime = TimeBetweenWave;
                        TimeBetweenSpawns -= 0.15f;
                    }
                    else{
                        tempTime = TimeBetweenSpawns+Random.Range(-SpawnTimeVariance,SpawnTimeVariance);
                    }
                }
            }
            else{
                tempTime -= Time.deltaTime;
                if(tempTime <= 0){
                    TempWaveStrength = WaveStrength;
                    tempTime = TimeBetweenSpawns+Random.Range(-SpawnTimeVariance,SpawnTimeVariance);
                    WaveActive = true;
                }
            }
        }

        
    }
}