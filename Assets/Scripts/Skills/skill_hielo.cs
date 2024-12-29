using UnityEngine;
using UnityEngine.UI;

public class SkillHielo : MonoBehaviour
{
    public float duracion = 5f;
    public float efectoRalentizacion = 0.5f;

    public GameObject botonHabilidad;

    private bool efectoActivo = false;

    public void ActivarHabilidad()
    {
        if (!efectoActivo)
        {
            StartCoroutine(RalentizarAliens());
        }
    }

    private System.Collections.IEnumerator RalentizarAliens()
    {
        efectoActivo = true;

        GameObject[] aliens = GameObject.FindGameObjectsWithTag("Enemigo");

        foreach (GameObject alien in aliens)
        {
            if (alien != null)
            {
                LogicaAlienTipo1 logica = alien.GetComponent<LogicaAlienTipo1>();
                LogicaAlienTipo2 logicaTipo2 = alien.GetComponent<LogicaAlienTipo2>();
                if (logica != null)
                {
                    logica.velocidadDescenso *= efectoRalentizacion;
                }
                if (logicaTipo2 != null)
                {
                    logicaTipo2.velocidadDescenso *= efectoRalentizacion;
                }
            }
        }

        if (botonHabilidad != null)
        {
            botonHabilidad.GetComponent<Button>().interactable = false;
            CambiaTransparenciaBoton(botonHabilidad, 0.5f);
        }

        yield return new WaitForSeconds(duracion);

        foreach (GameObject alien in aliens)
        {
            if (alien != null)
            {
                LogicaAlienTipo1 logica = alien.GetComponent<LogicaAlienTipo1>();
                if (logica != null)
                {
                    logica.velocidadDescenso /= efectoRalentizacion;
                }
            }
        }

        if (botonHabilidad != null)
        {
            botonHabilidad.GetComponent<Button>().interactable = true;
            CambiaTransparenciaBoton(botonHabilidad, 1f);
        }

        efectoActivo = false;
    }

    private void CambiaTransparenciaBoton(GameObject boton, float alpha)
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
