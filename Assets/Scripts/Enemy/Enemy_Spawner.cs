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
    [HideInInspector] public List<Enemy> enemyList;
    public List<WaveDescription> waves;
    private int currentWave = 0;

    private void Awake()
    {
        GameObject go = GameObject.Find("WavesCount");
        if (go) wavesLabel = go.GetComponent<TextMeshProUGUI>();
        if (wavesLabel) wavesLabel.gameObject.SetActive(false);
    }

    IEnumerator ShowWaveLabel(int waveNumber)
    {
        wavesLabel.SetText("Go Forward");
        wavesLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        wavesLabel.gameObject.SetActive(false);
    }

    public void InitiateEnemyWave()
    {
        for (int i = 0; i < waves[currentWave].NumberOfEnemies1; i++)
        {
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
                result = new Vector3(Random.Range(-7, 7), 1, carPos.z + 30);
                break;
            case WaveDescription.SpawnType.FullCircle:
                carPos.x = 0;
                result = carPos + RandomPointOnCircle(10);
                break;
            case WaveDescription.SpawnType.HalfFrontCircle:
                carPos.x = 0;
                result = carPos + RandomPointOnHalfFrontCircle(10);
                break;
            case WaveDescription.SpawnType.SideWays:
                carPos.x = 0;
                result = carPos + RandomPointOnSideWays(10);
                break;
            default:
                result = new Vector3(Random.Range(-7, 7), 1, carPos.z + 30);
                break;
        }
        return result;
    }


    private Vector3 RandomPointOnCircle(int radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        return new Vector3(x, 0, y);
    }

    private Vector3 RandomPointOnHalfFrontCircle(int radius)
    {
        float angle = Random.Range(0f, Mathf.PI);
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        return new Vector3(x, 0, y);
    }
    private Vector3 RandomPointOnSideWays(int radius)
    {
        int side = Random.Range(0, 2);
        Debug.Log("side " + side);
        float angle = Random.Range(-Mathf.PI / 180f * 20f, Mathf.PI / 180f * 20f) + Mathf.PI * side;
        Debug.Log("angle " + angle * 180);
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        return new Vector3(x, 0, y);
    }
}
