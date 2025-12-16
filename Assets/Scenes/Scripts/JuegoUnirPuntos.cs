using System.Collections.Generic;
using UnityEngine;

public class JuegoUnirPuntos : MonoBehaviour
{
    [Header("Configuración")]
    public LineRenderer linea; // El componente que dibuja
    public int siguienteNumeroEsperado = 1; // Empezamos buscando el 1

    [Header("Estado")]
    public bool dibujando = false;
    public Transform dedoSeguimiento; // Para saber dónde está el dedo
    private Vector3 ultimaPosicionValida;

    void Start()
    {
        // Inicializar la línea
        linea.positionCount = 0;
    }

    void Update()
    {
        // EFECTO DE LÍNEA: Si estamos jugando, el último punto de la línea sigue al dedo
        if (dibujando && dedoSeguimiento != null)
        {
            // Actualizamos la punta de la línea a la posición del dedo
            int indiceFinal = linea.positionCount - 1;
            linea.SetPosition(indiceFinal, dedoSeguimiento.position);
        }
    }

    public void IntentoTocarPunto(Punto puntoTocado, Transform dedo)
    {
        // 1. ¿Es el número que esperábamos?
        if (puntoTocado.numeroID == siguienteNumeroEsperado)
        {
            // ¡CORRECTO!
            puntoTocado.ActivarPunto();

            // Guardamos la referencia del dedo para seguirlo
            dedoSeguimiento = dedo;

            // Actualizamos la línea
            AgregarPuntoALinea(puntoTocado.transform.position);

            // Preparamos para el siguiente
            siguienteNumeroEsperado++;
            dibujando = true;
            ultimaPosicionValida = puntoTocado.transform.position;

            Debug.Log("Punto " + puntoTocado.numeroID + " conectado!");
        }
    }

    void AgregarPuntoALinea(Vector3 posicion)
    {
        // Si es el primer punto (1), necesitamos 2 vértices:
        // Uno fijo en el punto, y otro móvil que siga al dedo.
        if (siguienteNumeroEsperado == 1)
        {
            linea.positionCount = 2;
            linea.SetPosition(0, posicion); // Fijo
            linea.SetPosition(1, posicion); // Móvil (se actualizará en Update)
        }
        else
        {
            // Si ya no es el primero, "congelamos" el segmento anterior
            // y añadimos un nuevo punto móvil para el siguiente tramo.
            int puntosActuales = linea.positionCount;
            linea.positionCount = puntosActuales + 1;

            // Fijamos el anterior en su lugar exacto
            linea.SetPosition(puntosActuales - 1, posicion);

            // El nuevo último punto empieza aquí pero se moverá con el dedo
            linea.SetPosition(puntosActuales, posicion);
        }
    }
}