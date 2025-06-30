using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] GameObject[] points;  // Array di waypoint
    [SerializeField] GameObject startPos;
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 5f;

    [Header("Enemy")]
    [SerializeField] GameObject enemyBody;

    [Header("Ground Detection")]
    [SerializeField] float groundOffset = 0.1f;
    [SerializeField] float raycastDistance = 2f;
    [SerializeField] LayerMask groundLayer;

    private int index = 0;
    private GameObject nextPos;

    void Start()
    {
        if (points == null || points.Length == 0)
        {
            Debug.LogError("Waypoints non assegnati!");
            enabled = false;
            return;
        }

        if (startPos != null)
        {
            enemyBody.transform.position = startPos.transform.position;
        }
        else
        {
            startPos = points[0];
            enemyBody.transform.position = startPos.transform.position;
        }

        index = 1 % points.Length;
        nextPos = points[index];
    }

    void Update()
    {
        MoveEnemy();
        AlignWithGround();
    }

    void MoveEnemy()
    {
        Vector3 targetPos = new Vector3(nextPos.transform.position.x, enemyBody.transform.position.y, nextPos.transform.position.z);
        Vector3 direction = (targetPos - enemyBody.transform.position).normalized;

        // Rotazione fluida verso il waypoint
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyBody.transform.rotation = Quaternion.Slerp(enemyBody.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        enemyBody.transform.position = Vector3.MoveTowards(enemyBody.transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(enemyBody.transform.position, targetPos) < 0.1f)
        {
            index = (index + 1) % points.Length;
            nextPos = points[index];
        }
    }

    void AlignWithGround()
    {
        Vector3 origin = enemyBody.transform.position + Vector3.up * 0.5f;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, raycastDistance, groundLayer))
        {
            Vector3 pos = enemyBody.transform.position;
            pos.y = hit.point.y + groundOffset;
            enemyBody.transform.position = pos;
        }
    }
}
