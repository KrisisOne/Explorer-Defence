using UnityEngine;

public class skill_hielo : MonoBehaviour
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
                if (logica != null)
                {
                    logica.velocidadDescenso *= efectoRalentizacion;
                }
            }
        }

        if (botonHabilidad != null)
        {
            botonHabilidad.SetActive(false);
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
            botonHabilidad.SetActive(true);
        }

        efectoActivo = false;
    }
}