
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCarController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Movimento in avanti
        Vector3 move = transform.forward * moveInput * speed * Time.fixedDeltaTime;

        // Rotazione
        Quaternion turn = Quaternion.Euler(0, turnInput * turnSpeed * Time.fixedDeltaTime, 0);

        // Muovi con fisica
        rb.MovePosition(rb.position + move);
        rb.MoveRotation(rb.rotation * turn);
    }
}
