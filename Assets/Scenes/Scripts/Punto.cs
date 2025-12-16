using UnityEngine;
using TMPro;

public class Punto : MonoBehaviour
{
    public int numeroID; // 1, 2, 3...
    public bool yaFueTocado = false;

    [Header("Referencias")]
    public TextMeshPro textoNumero;
    public Material materialActivado; // Color cuando se toca (ej. Verde)

    // Referencia al Manager (se busca solo al iniciar)
    private JuegoUnirPuntos manager;

    void Start()
    {
        manager = FindObjectOfType<JuegoUnirPuntos>();
        if (textoNumero != null) textoNumero.text = numeroID.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        // Solo activamos si es la mano y el punto no ha sido tocado
        if (!yaFueTocado && (other.name.Contains("index") || other.name.Contains("bone3")))
        {
            // Avisamos al manager que tocaron este punto
            // Enviamos la posición exacta del dedo (other.transform.position)
            manager.IntentoTocarPunto(this, other.transform);
        }
    }

    public void ActivarPunto()
    {
        yaFueTocado = true;
        GetComponent<Renderer>().material = materialActivado;
        // Aquí podrías poner un sonido de "Ding"
    }
}