using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorIntro : MonoBehaviour
{
    [Header("Configuración")]
    public float duracionIntro = 3.0f; // Cuánto dura todo el proceso
    public string nombreEscenaMenu = "MenuPrincipal";

    [Header("Referencias Visuales")]
    public CanvasGroup grupoUI; // Para controlar la transparencia
    public RectTransform logoYTexto; // Para controlar el tamaño (Scale)

    void Start()
    {
        // Iniciamos con transparencia 0 y tamaño un poco pequeño
        grupoUI.alpha = 0;
        if (logoYTexto != null)
        {
            logoYTexto.localScale = new Vector3(0.8f, 0.8f, 1f); // Empieza al 80% de tamaño
        }

        // Arrancamos la animación
        StartCoroutine(AnimacionYCambio());
    }

    IEnumerator AnimacionYCambio()
    {
        float tiempoPasado = 0f;

        // BUCLE DE ANIMACIÓN: Mientras no haya pasado el tiempo...
        while (tiempoPasado < duracionIntro)
        {
            tiempoPasado += Time.deltaTime;

            // Calculamos el porcentaje de avance (de 0 a 1)
            float porcentaje = tiempoPasado / duracionIntro;

            // 1. Efecto Fade In (Aparición)
            // Hacemos que el Alpha suba suavemente
            grupoUI.alpha = Mathf.Lerp(0, 1, porcentaje);

            // 2. Efecto Zoom (Crecimiento)
            // Hacemos que la escala vaya de 0.8 a 1.0
            if (logoYTexto != null)
            {
                float escalaActual = Mathf.Lerp(0.8f, 1.0f, porcentaje);
                logoYTexto.localScale = new Vector3(escalaActual, escalaActual, 1f);
            }

            // Esperamos al siguiente frame
            yield return null;
        }

        // Aseguramos que termine al 100% por si acaso
        grupoUI.alpha = 1;

        // Esperamos medio segundo extra para que el usuario admire el logo
        yield return new WaitForSeconds(0.5f);

        // ¡Cambio de escena!
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}