using UnityEngine;

public class RandomScatter : MonoBehaviour
{
    public GameObject[] plantPrefabs;
    public int amount = 20;
    public Vector2 areaSize = new Vector2(10, 10);
    public LayerMask groundLayer;

    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                10f,
                Random.Range(-areaSize.y / 2, areaSize.y / 2)
            );

            pos += transform.position;

            if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit, 50f, groundLayer))
            {
                GameObject plant = Instantiate(
                    plantPrefabs[Random.Range(0, plantPrefabs.Length)],
                    hit.point,
                    Quaternion.Euler(0, Random.Range(0, 360), 0)
                );

                plant.transform.localScale *= Random.Range(0.8f, 1.2f);
                plant.transform.parent = transform;
            }
        }
    }
}