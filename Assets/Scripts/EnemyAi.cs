using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyAi : MonoBehaviour
{
    [SerializeField] private GameObject mutationPrefab;
    [SerializeField] private Vector2 moveDir = new(1,0);
    [SerializeField] private float speed = 3.5f;

    private void Update()
    {
        transform.Translate(moveDir * (speed * Time.deltaTime));
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
	        other.gameObject.GetComponent<Health>().LoseHealth();
	        moveDir *= -1;
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
	        moveDir *= -1;
        }
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        Instantiate(mutationPrefab, transform.position, Quaternion.identity);
    }
}