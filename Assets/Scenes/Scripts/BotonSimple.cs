using UnityEngine;

public class BotonSimple : MonoBehaviour
{
    // Esta función especial de Unity se llama cuando otro Collider entra en nuestro Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Imprimimos un mensaje en la consola para saber que funcionó
        Debug.Log("¡TOCADO! El objeto que me tocó se llama: " + other.name);
    }

    // --- Vamos a añadir feedback visual ---

    private Renderer miRenderer;
    private Color colorOriginal;

    void Start()
    {
        // Obtenemos el componente Renderer para poder cambiar de color
        miRenderer = GetComponent<Renderer>();
        colorOriginal = miRenderer.material.color;
    }

    // Se llama cuando la mano entra
    private void OnTriggerStay(Collider other)
    {
        // Cambiamos el color a verde mientras la mano esté dentro
        miRenderer.material.color = Color.green;
    }

    // Se llama cuando la mano sale
    private void OnTriggerExit(Collider other)
    {
        // Volvemos al color original
        miRenderer.material.color = colorOriginal;
    }
}
