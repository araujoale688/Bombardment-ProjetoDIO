using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public List<GameObject> bombPrefabs;
    public GameObject spawnPoint;
    public GameObject target;
    public Vector2 timeInterval = new Vector2 (1, 1);
    public Vector2 force;

    public float rangeInDegrees;
    public float arcDegrees = 45f;

    private float cooldown;

    void Start()
    {
        cooldown = Random.Range(timeInterval.x, timeInterval.y);
    }

    void Update()
    {
        if(GameManager.Instance.isGameOver)
        {
            return;
        }

        cooldown -= Time.deltaTime;

        if(cooldown < 0)
        {
            cooldown = Random.Range(timeInterval.x, timeInterval.y);

            Fire();
        }
    }

    private void Fire()
    {
        //Criar Bomb.
        GameObject bombPrefab = bombPrefabs[Random.Range(0, bombPrefabs.Count)];

        GameObject bomb = Instantiate(bombPrefab, spawnPoint.transform.position, bombPrefab.transform.rotation);

        //Atirando.
        Rigidbody rigidbodyBomb = bomb.GetComponent<Rigidbody>();

        Vector3 impulseVector = target.transform.position - spawnPoint.transform.position;

        impulseVector.Scale(new Vector3(1, 0, 1));
        impulseVector.Normalize();

        impulseVector += new Vector3(0, arcDegrees / 45f, 0);
        impulseVector.Normalize();

        impulseVector = Quaternion.AngleAxis(rangeInDegrees * Random.Range(-1f, 1f), Vector3.up) * impulseVector;
        impulseVector *= Random.Range(force.x, force.y);

        rigidbodyBomb.AddForce(impulseVector, ForceMode.Impulse);
    }
}