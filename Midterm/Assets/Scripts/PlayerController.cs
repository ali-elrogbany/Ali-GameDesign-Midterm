using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private int speed;

    [Header("Local Variables")]
    private float xMovement;
    private float yMovement;

    [Header("References")]
    [SerializeField] private GameObject particlesPrefab;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(new Vector3(xMovement, 0, yMovement) * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        CollectableController collectableController;

        gameObject.TryGetComponent<CollectableController>(out collectableController);

        if (collectableController)
        {
            int currentScore = GameManager.instance.GetScore();
            if (collectableController.GetCollectableType() == CollectableType.CORRECT)
            {
                GameManager.instance.SetScore(currentScore + 1);
                SoundManager.instance.PlayCorrectSfx();
            }

            else if (collectableController.GetCollectableType() == CollectableType.INCORRECT)
            {
                if (currentScore > 0)
                {
                    GameManager.instance.SetScore(currentScore - 1);
                }
                SoundManager.instance.PlayIncorrectSfx();
            }

            Destroy(gameObject);
            CollectableSpawner.instance.SpawnRandomCollectable();

            GameObject newParticlePrefab = Instantiate(particlesPrefab, transform.position, Quaternion.identity);
            newParticlePrefab.GetComponent<ParticleSystem>()?.Play();
        }
    }
}
