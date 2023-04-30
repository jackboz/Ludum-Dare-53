using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] Crate cart;
    [SerializeField] TextMeshProUGUI wavesLabel;
    [SerializeField] private Enemy prefab;
    [SerializeField] private Enemy prefab2;
    [SerializeField] private int enemyCount;
    [SerializeField] private int enemyCount2;
    [HideInInspector] public List<Enemy> enemyList;
    [SerializeField] private float pauseBetweenWaves = 8f;
    public List<WaveDescription> waves;
    private int currentWave = 0;
    private float timer = 0;

    private void Awake()
    {
        GameObject go = GameObject.Find("WavesCount");
        if (go) wavesLabel = go.GetComponent<TextMeshProUGUI>();
        if (wavesLabel) wavesLabel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (enemyList.Count == 0)
        {
            if (currentWave < waves.Count)
            {
                timer += Time.deltaTime;
                if (timer > pauseBetweenWaves)
                {
                    Debug.Log("New wave " + currentWave);
                    InitiateEnemyWave();
                    timer = 0;
                }
            }
        }
    }

    IEnumerator WaitForSecs()
    {
        yield return new WaitForSeconds(pauseBetweenWaves);
    }
    IEnumerator ShowWaveLabel(int waveNumber)
    {
        wavesLabel.SetText("Wave " + waveNumber);
        wavesLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        wavesLabel.gameObject.SetActive(false);
    }

    private void InitiateEnemyWave()
    {
        for (int i = 0; i < waves[currentWave].NumberOfEnemies1; i++)
        {
            //Vector3 spawnPos = new Vector3(Random.Range(-7, 7), 1, carPos.z + 40);
            enemyList.Add(Instantiate(prefab, GetRandomSpawnPos(), Quaternion.identity));
        }

        for (int i = 0; i < waves[currentWave].NumberOfRangedEnemies; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPos();
            enemyList.Add(Instantiate(prefab2, spawnPos, Quaternion.identity));
        }
        if (wavesLabel) StartCoroutine(ShowWaveLabel(currentWave + 1));
        currentWave += 1;
    }

    private Vector3 GetRandomSpawnPos()
    {
        Vector3 result;
        Vector3 carPos = cart.transform.position;
        switch (waves[currentWave].spawnType)
        {
            case WaveDescription.SpawnType.Front:
                result = new Vector3(Random.Range(-7, 7), 1, carPos.z + 40);
                break;
            case WaveDescription.SpawnType.FullCircle:
                carPos.x = 0;
                result = carPos + RandomPointOnCircle(12);
                break;
            case WaveDescription.SpawnType.HalfFrontCircle:
                carPos.x = 0;
                result = carPos + RandomPointOnHalfFrontCircle(12);
                break;
            default:
                result = new Vector3(Random.Range(-7, 7), 1, carPos.z + 40);
                break;
        }

        return result;
    }


    private Vector3 RandomPointOnCircle(int radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2); // Random angle in radians

        // Calculate the position on the circumference using trigonometry
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        return new Vector3(x, 0, y);
    }

    private Vector3 RandomPointOnHalfFrontCircle(int radius)
    {
        float angle = Random.Range(0f, Mathf.PI); // Random angle in radians

        // Calculate the position on the circumference using trigonometry
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        return new Vector3(x, 0, y);
    }

}
