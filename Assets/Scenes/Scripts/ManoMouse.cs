using UnityEngine;
using UnityEngine.EventSystems; // Necesario para hacer clicks
using UnityEngine.UI;
using System.Collections.Generic;

public class ManoMouse : MonoBehaviour
{
    [Header("Configuración")]
    public RectTransform cursorVisual; // La imagen roja que creamos
    public Camera camaraUI; // Tu Main Camera

    [Header("Referencias Leap")]
    public GameObject manoDerecha; // Arrastra el objeto "Capsule Hand Right" aquí

    // Variables internas
    private PointerEventData pointerData;
    private List<RaycastResult> raycastResults;
    private bool estabaPellizcando = false;

    void Start()
    {
        pointerData = new PointerEventData(EventSystem.current);
        raycastResults = new List<RaycastResult>();
    }

    void Update()
    {
        // 1. Encontrar la punta del dedo índice
        // Buscamos el objeto "index_end" o "index_distal" dentro de la mano
        Transform puntaIndice = EncontrarPuntaDedo(manoDerecha.transform);

        if (puntaIndice != null && manoDerecha.activeInHierarchy)
        {
            cursorVisual.gameObject.SetActive(true);

            // 2. Convertir posición 3D a Pantalla 2D
            Vector3 screenPos = camaraUI.WorldToScreenPoint(puntaIndice.position);
            cursorVisual.position = screenPos;

            // 3. Simular el Clic con "Pellizco" (Pinch)
            // (Aquí usamos un truco simple: si el pulgar e índice están cerca)
            bool estaPellizcando = DetectarPellizco();

            if (estaPellizcando && !estabaPellizcando)
            {
                HacerClic(screenPos);
            }

            // Guardamos estado para el siguiente frame
            estabaPellizcando = estaPellizcando;
        }
        else
        {
            // Si no hay mano, escondemos el cursor
            cursorVisual.gameObject.SetActive(false);
        }
    }

    // Función auxiliar para buscar el dedo sin errores
    Transform EncontrarPuntaDedo(Transform mano)
    {
        // Buscamos recursivamente el hueso final del índice
        Transform[] hijos = mano.GetComponentsInChildren<Transform>();
        foreach (Transform t in hijos)
        {
            if (t.name.Contains("index") && (t.name.Contains("end") || t.name.Contains("tip") || t.name.Contains("distal")))
            {
                return t;
            }
        }
        return null;
    }

    bool DetectarPellizco()
    {
        // Buscamos pulgar e índice
        Transform indice = EncontrarPuntaDedo(manoDerecha.transform);
        Transform pulgar = null;

        Transform[] hijos = manoDerecha.GetComponentsInChildren<Transform>();
        foreach (Transform t in hijos)
        {
            if (t.name.Contains("thumb") && (t.name.Contains("end") || t.name.Contains("tip") || t.name.Contains("distal")))
                pulgar = t;
        }

        if (indice != null && pulgar != null)
        {
            // Si están a menos de 2 cm, es un pellizco
            if (Vector3.Distance(indice.position, pulgar.position) < 0.02f)
                return true;
        }
        return false;
    }

    void HacerClic(Vector2 pos)
    {
        pointerData.position = pos;
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            // Intentamos hacer clic en lo que encontremos (Botones, Inputs)
            ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);

            // Especial para InputFields: Forzamos la selección para que aparezca el cursor de texto
            if (result.gameObject.GetComponent<InputField>() || result.gameObject.GetComponent<TMPro.TMP_InputField>())
            {
                EventSystem.current.SetSelectedGameObject(result.gameObject);
            }
        }
        raycastResults.Clear();
    }
}