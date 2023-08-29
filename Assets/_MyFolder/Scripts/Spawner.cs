using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 15f;
    public float maxForce = 25f;

    public float maxLifetime = 1f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);    //Waits time before fruit start spawning

        while (enabled)
        {
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];  //Spawns random fruit

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);  
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);  
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);  //Spawns the fruit at random within this space

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));    //Angle of where the fruit spawn

            GameObject fruit = Instantiate(prefab, position, rotation);     //Spawns Fruit
            Destroy(fruit, maxLifetime);    //Destroys Fruit

            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);    //Launching fruit

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));    //Time between fruit spawns
        }
    }

}
