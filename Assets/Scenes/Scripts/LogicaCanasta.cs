using UnityEngine;
using TMPro; // ¡Importante! Necesario para usar textos modernos
using System.Collections;

public class LogicaCanasta : MonoBehaviour
{
    [Header("Configuración")]
    public string etiquetaCorrecta; // "Fruta" o "Juguete"

    [Header("Feedback Visual")]
    public TextMeshPro textoFeedback; // Aquí arrastraremos el texto 3D que creaste
    public float tiempoDeEspera = 1.0f; // Tiempo antes de destruir el objeto

    // Colores para el texto (puedes cambiarlos en el inspector)
    public Color colorAcierto = Color.green;
    public Color colorError = Color.red;

    private void OnTriggerEnter(Collider other)
    {
        // CASO 1: Correcto
        if (other.gameObject.CompareTag(etiquetaCorrecta))
        {
            Debug.Log("¡Correcto!");
            MostrarMensaje("¡Muy bien!", colorAcierto);

            // Opcional: Destruir el objeto o desactivar su física para que no moleste
            // Destroy(other.gameObject, 0.5f); 
            ControladorPuntaje.Instancia.SumarPunto(etiquetaCorrecta);
        }

        // CASO 2: Incorrecto (Pero es una pieza del juego)
        else if (other.gameObject.CompareTag("Fruta") || other.gameObject.CompareTag("Juguete"))
        {
            Debug.Log("Incorrecto");
            MostrarMensaje("¡No has elegido bien!", colorError);

            // Destruir el objeto erróneo
            Destroy(other.gameObject, tiempoDeEspera);
        }
    }

    // Función auxiliar para manejar el texto
    void MostrarMensaje(string mensaje, Color colorTexto)
    {
        if (textoFeedback != null)
        {
            textoFeedback.text = mensaje;
            textoFeedback.color = colorTexto;

            // Detenemos cualquier borrado anterior y empezamos uno nuevo
            StopAllCoroutines();
            StartCoroutine(BorrarTextoDespuesDeTiempo());
        }
    }

    // Rutina para que el texto desaparezca solo después de 2 segundos
    IEnumerator BorrarTextoDespuesDeTiempo()
    {
        yield return new WaitForSeconds(2.0f); // El mensaje dura 2 segundos visible
        if (textoFeedback != null)
        {
            textoFeedback.text = ""; // Borramos el texto
        }
    }
}