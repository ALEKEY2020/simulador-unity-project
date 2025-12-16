using UnityEngine;

public class Cinta : MonoBehaviour
{
    public float speed = 0.2f; // Velocidad de la cinta

    // Esta funci�n se llama en cada paso de f�sica
    // mientras otro objeto est� tocando la cinta.
    void OnCollisionStay(Collision other)
    {
        // Revisa si el objeto que nos toca tiene un Rigidbody
        Rigidbody rb = other.rigidbody;
        if (rb != null)
        {
            // Calcula la velocidad que queremos
            // (transform.forward es la direcci�n "adelante" de la cinta)
            Vector3 targetVelocity = -transform.forward * speed;

            // Importante: �No queremos afectar la gravedad!
            // Mantenemos la velocidad Y (vertical) que ya ten�a el objeto.
            targetVelocity.y = rb.linearVelocity.y;

            // Aplicamos la nueva velocidad
            rb.linearVelocity = targetVelocity;
        }
    }
}