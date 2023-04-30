using UnityEngine;

public class ParcelManager : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private int amountOfParcels;
    [SerializeField] private GameObject[] inBetween;
    [SerializeField] private GameObject end;

    private void Start()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        Instantiate(start, pos,Quaternion.identity);
        for(int i = 0; i < amountOfParcels; i++)
        {
            pos.z += 25;
            Instantiate(inBetween[Random.Range(0,inBetween.Length)] , pos, Quaternion.identity);
        }
        pos.z += 25;
        Instantiate(end, pos, Quaternion.identity);
    }
}
