using UnityEngine;
using UnityEngine.UI;

public class HabilidadHielo : MonoBehaviour
{
    public float duracion = 5f;
    public float efectoRalentizacion = 0.5f;

    public GameObject botonHabilidad;
    public LogicaAlienTipo1[] aliens;

    private bool habilidadActiva = false;

    public void ActivarHabilidad()
    {
        if (!habilidadActiva)
        {
            StartCoroutine(AplicarHabilidadHielo());
        }
    }

    private System.Collections.IEnumerator AplicarHabilidadHielo()
    {
        habilidadActiva = true;

        GameObject[] aliens = GameObject.FindGameObjectsWithTag("Enemigo");
        foreach (GameObject alien in aliens)
        {
            LogicaAlienTipo1 logica = alien.GetComponent<LogicaAlienTipo1>();
            if (logica != null)
            {
                logica.velocidadDescenso *= efectoRalentizacion;
            }
        }

        if (botonHabilidad != null)
        {
            CambiarTransparenciaBoton(botonHabilidad, 0.5f);
            botonHabilidad.GetComponent<Button>().interactable = false;
        }

        yield return new WaitForSeconds(duracion);

        foreach (GameObject alien in aliens)
        {
            LogicaAlienTipo1 logica = alien.GetComponent<LogicaAlienTipo1>();
            if (logica != null)
            {
                logica.velocidadDescenso /= efectoRalentizacion;
            }
        }

        if (botonHabilidad != null)
        {
            CambiarTransparenciaBoton(botonHabilidad, 1f);
            botonHabilidad.GetComponent<Button>().interactable = true;
        }

        habilidadActiva = false;
    }

    private void CambiarTransparenciaBoton(GameObject boton, float alpha)
    {
        Image botonImagen = boton.GetComponent<Image>();
        if (botonImagen != null)
        {
            Color colorActual = botonImagen.color;
            colorActual.a = alpha;
            botonImagen.color = colorActual;
        }
    }
}
