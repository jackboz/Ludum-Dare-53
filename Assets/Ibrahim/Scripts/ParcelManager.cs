using UnityEngine;

public class ParcelManager : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private int amountOfParcels;
    [SerializeField] private GameObject[] inBetween;
    [SerializeField] private GameObject end;

    private void Start()
    {
        Vector3 pos = new Vector3(0, 0, -20);
        Instantiate(start, pos, Quaternion.identity);
        pos.z += 18.8f;
        GameObject parcel = Instantiate(inBetween[0], pos, Quaternion.identity);
        Transform trigger = parcel.transform.Find("Trigger");
        if (trigger) trigger.gameObject.SetActive(false);
        for (int i = 1; i < amountOfParcels; i++)
        {
            pos.z += 18.8f;
            Instantiate(inBetween[Random.Range(0, inBetween.Length)], pos, Quaternion.identity);
        }
        pos.z += 18.8f;
        Instantiate(end, pos, Quaternion.identity);
    }
}
