using UnityEngine;
using UnityEngine.UI;

public class SkillBomba : MonoBehaviour
{
    public int daño = 300;
    public string tagEnemigos = "Enemigo";
    public GameObject efectoExplosion;
    public GameObject botonHabilidad;
    public float duracion = 120f;

    private bool bombaUsada = false;

    public void ActivarBomba()
    {
        if (!bombaUsada)
        {
            StartCoroutine(EjecutarBomba());
        }
    }

    private System.Collections.IEnumerator EjecutarBomba()
    {
        bombaUsada = true;

        if (botonHabilidad != null)
        {
            botonHabilidad.GetComponent<Button>().interactable = false;
            CambiarTransparenciaBoton(botonHabilidad, 0.5f);
        }

        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(tagEnemigos);

        foreach (GameObject enemigo in enemigos)
        {
            if (enemigo != null)
            {
                LogicaAlienTipo1 logica = enemigo.GetComponent<LogicaAlienTipo1>();
                LogicaAlienTipo2 logica2 = enemigo.GetComponent<LogicaAlienTipo2>();
                if (logica != null)
                {
                    logica.RecibirDaño(daño);
                    logica2.RecibirDaño(daño);
                }
            }
        }

        if (efectoExplosion != null)
        {
            Instantiate(efectoExplosion, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(duracion);

        if (botonHabilidad != null)
        {
            botonHabilidad.GetComponent<Button>().interactable = true;
            CambiarTransparenciaBoton(botonHabilidad, 1f);
        }
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
