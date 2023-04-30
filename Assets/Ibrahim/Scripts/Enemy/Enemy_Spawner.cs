using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] Crate cart;
    [SerializeField] private Enemy prefab;
    [SerializeField] private Enemy prefab2;
    [SerializeField] private int enemyCount;
    [SerializeField] private int enemyCount2;
    [HideInInspector] public List<Enemy> enemyList;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitUntil(() => enemyList.Count == 0);
            yield return new WaitForSeconds(8);


            enemyCount = Mathf.Clamp(enemyCount + 1, 1, 6);

            for (int i = 0; i < enemyCount; i++)
            {
                //Vector3 spawnPos = new Vector3(Random.Range(-7, 7), 1, carPos.z + 40);
                enemyList.Add(Instantiate(prefab, GetRandomSpawnPos(), Quaternion.identity));
            }

            if (enemyCount > 4)
            {
                enemyCount2 = Mathf.Clamp(enemyCount2 + 1, 1, 3);
                for (int i = 0; i < enemyCount2; i++)
                {
                    Vector3 spawnPos = GetRandomSpawnPos();
                    enemyList.Add(Instantiate(prefab2, spawnPos, Quaternion.identity));
                }
            }
        }


    }
    private Vector3 GetRandomSpawnPos()
    {
        Vector3 carPos = cart.transform.position;
        carPos.x = 0;
        return carPos + RandomPointOnCircle(12);
    }


    private Vector3 RandomPointOnCircle(int radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2); // Random angle in radians

        // Calculate the position on the circumference using trigonometry
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        return new Vector3(x, 0, y);
    }
}
