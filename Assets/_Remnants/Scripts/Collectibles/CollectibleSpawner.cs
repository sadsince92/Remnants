
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [Header("Collectibles")]
    [SerializeField] private GameObject[] collectibles;

    [Header("Spawn Locations")]
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SpawnCollectibles();
    }

    private void SpawnCollectibles()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randomIndex = Random.Range(0, collectibles.Length);

            GameObject selectedCollectible = collectibles[randomIndex];

            Instantiate(selectedCollectible, spawnPoints[i].position, Quaternion.identity);
        }
    }
}

