using UnityEngine;
using UnityEngine.SceneManagement; // ¡Obligatorio para cambiar escenas!
using System.Collections;

public class GameManagerSimple : MonoBehaviour
{
    public float retraso = 0.6f;
    // Esta función cargará tu juego de las esferas
    public void CargarFabricaDeEsferas()
    {
        Debug.Log("¡Cargando Fábrica de Esferas!");

        // Asegúrate de escribir el nombre EXACTO de tu escena aquí
        // Si la guardaste como "FabricaDeEsferas", ponlo así.
        //SceneManager.LoadScene("FabricaDeEsferas");
        StartCoroutine(EsperarYCargar("FabricaDeEsferas"));
    }

    public void CargarUnirPuntos()
    {
        Debug.Log("¡Cargando Unir Puntos!");

        // Asegúrate de escribir el nombre EXACTO de tu escena aquí
        // Si la guardaste como "FabricaDeEsferas", ponlo así.
        //SceneManager.LoadScene("FabricaDeEsferas");
        StartCoroutine(EsperarYCargar("UnirPuntos"));
    }

    public void CargarAtraparLuciernaga()
    {
        Debug.Log("¡Cargando Unir Puntos!");

        // Asegúrate de escribir el nombre EXACTO de tu escena aquí
        // Si la guardaste como "FabricaDeEsferas", ponlo así.
        //SceneManager.LoadScene("FabricaDeEsferas");
        StartCoroutine(EsperarYCargar("AtraparLuciernaga"));
    }

    public void CargarPonerMesa()
    {
        Debug.Log("¡Cargando Unir Puntos!");

        // Asegúrate de escribir el nombre EXACTO de tu escena aquí
        // Si la guardaste como "FabricaDeEsferas", ponlo así.
        //SceneManager.LoadScene("FabricaDeEsferas");
        StartCoroutine(EsperarYCargar("PonerMesa"));
    }
    public void RegresarAlMenuJuegos()
    {
        Debug.Log("Saliendo del juego...");
        StartCoroutine(EsperarYCargar("MenuPrincipal"));
    }

    IEnumerator EsperarYCargar(string nombreEscena)
    {
        Debug.Log("Sonido sonando... esperando " + retraso + " segundos.");

        // 1. Pausa el código aquí por 0.5 segundos
        yield return new WaitForSeconds(retraso);

        // 2. Ahora sí, carga la escena
        SceneManager.LoadScene(nombreEscena);
    }


}