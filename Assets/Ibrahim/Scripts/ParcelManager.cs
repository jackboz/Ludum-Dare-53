using UnityEngine;

public class ParcelManager : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject[] inBetween;
    [SerializeField] private GameObject end;

    private void Start()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        Instantiate(start, pos,Quaternion.identity);
        for(int i = 0; i < inBetween.Length; i++)
        {
            pos.z += 25;
            Instantiate(inBetween[i] , pos, Quaternion.identity);
        }
        pos.z += 25;
        Instantiate(end, pos, Quaternion.identity);
    }
}
