using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections; // Necesario para la animación suave

public class ControladorPuntaje : MonoBehaviour
{
    public static ControladorPuntaje Instancia;

    [Header("Referencias de UI Marcador")]
    public TextMeshProUGUI textoContadorFrutas;
    public TextMeshProUGUI textoContadorJuguetes;

    [Header("Referencias UI Fin del Juego")]
    public GameObject panelFinJuego;
    public GameObject spawnerObj;

    [Header("Configuración de Cámara Final")]
    public Camera camaraPrincipal; // Arrastra tu Main Camera aquí
    public Transform puntoEnfoqueFinal; // Arrastra el objeto "PosicionFinalCamara"
    public float velocidadTransicion = 2.0f; // Cuántos segundos tarda en moverse

    [Header("Configuración Juego")]
    public int metaObjetos = 10;
    public string nombreEscenaMenu = "MenuPrincipal";

    private int frutasActuales = 0;
    private int juguetesActuales = 0;
    private bool juegoTerminado = false;

    void Awake()
    {
        if (Instancia == null) Instancia = this;
    }

    void Start()
    {
        if (panelFinJuego != null) panelFinJuego.SetActive(false);
        ActualizarTextos();
    }

    public void SumarPunto(string tipo)
    {
        if (juegoTerminado) return;

        if (tipo == "Fruta")
        {
            frutasActuales++;
            if (frutasActuales > metaObjetos) frutasActuales = metaObjetos;
        }
        else if (tipo == "Juguete")
        {
            juguetesActuales++;
            if (juguetesActuales > metaObjetos) juguetesActuales = metaObjetos;
        }

        ActualizarTextos();
        VerificarVictoria();
    }

    void ActualizarTextos()
    {
        textoContadorFrutas.text = frutasActuales + " / " + metaObjetos;
        textoContadorJuguetes.text = juguetesActuales + " / " + metaObjetos;
    }

    void VerificarVictoria()
    {
        if (frutasActuales >= metaObjetos && juguetesActuales >= metaObjetos)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        juegoTerminado = true;

        // 1. Mostrar panel y apagar spawner
        if (panelFinJuego != null) panelFinJuego.SetActive(true);
        if (spawnerObj != null) spawnerObj.SetActive(false);

        // 2. Iniciar el movimiento de cámara
        if (camaraPrincipal != null && puntoEnfoqueFinal != null)
        {
            StartCoroutine(MoverCamaraSuavemente());
        }
    }

    // Esta rutina mueve la cámara poco a poco
    IEnumerator MoverCamaraSuavemente()
    {
        float tiempoTranscurrido = 0;

        // Guardamos dónde estaba la cámara al principio
        Vector3 posicionInicial = camaraPrincipal.transform.position;
        Quaternion rotacionInicial = camaraPrincipal.transform.rotation;

        while (tiempoTranscurrido < velocidadTransicion)
        {
            // Calculamos el progreso (de 0 a 1)
            float t = tiempoTranscurrido / velocidadTransicion;

            // Suavizado del movimiento (Easy In - Easy Out)
            t = t * t * (3f - 2f * t);

            // Interpolamos posición y rotación
            camaraPrincipal.transform.position = Vector3.Lerp(posicionInicial, puntoEnfoqueFinal.position, t);
            camaraPrincipal.transform.rotation = Quaternion.Lerp(rotacionInicial, puntoEnfoqueFinal.rotation, t);

            tiempoTranscurrido += Time.deltaTime;
            yield return null; // Esperar al siguiente frame
        }

        // Aseguramos que termine exactamente en el punto final
        camaraPrincipal.transform.position = puntoEnfoqueFinal.position;
        camaraPrincipal.transform.rotation = puntoEnfoqueFinal.rotation;
    }

    // --- BOTONES ---
    public void BotonAccionReintentar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BotonAccionMenu()
    {
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}