using UnityEngine;
using UnityEngine.UI;

public class SkillMunicion : MonoBehaviour
{
    public float duracion = 5f;
    public float multiplicadorVelocidadAtaque = 1.5f;
    public float multiplicadorDaño = 2f;
    public float multiplicadorVelocidadProyectil = 1.5f;

    public LogicaBlaster blaster;
    public LogicaDisparos disparoPrefab;
    public GameObject botonHabilidad;

    private bool habilidadActiva = false;

    public void ActivarHabilidad()
    {
        if (!habilidadActiva)
        {
            StartCoroutine(MejorarAtaqueCompleto());
        }
    }

    private System.Collections.IEnumerator MejorarAtaqueCompleto()
    {
        habilidadActiva = true;

        float velocidadAtaqueOriginal = blaster.atkSpeed;
        int dañoOriginal = disparoPrefab.daño;
        float velocidadProyectilOriginal = disparoPrefab.GetComponent<Rigidbody2D>().linearVelocity.magnitude;

        blaster.atkSpeed *= multiplicadorVelocidadAtaque;
        disparoPrefab.daño = Mathf.RoundToInt(disparoPrefab.daño * multiplicadorDaño);

        Rigidbody2D rb = disparoPrefab.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity *= multiplicadorVelocidadProyectil;
        }

        if (botonHabilidad != null)
        {
            botonHabilidad.GetComponent<Button>().interactable = false;
        }

        yield return new WaitForSeconds(duracion);

        blaster.atkSpeed = velocidadAtaqueOriginal;
        disparoPrefab.daño = dañoOriginal;

        if (rb != null)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * velocidadProyectilOriginal;
        }

        if (botonHabilidad != null)
        {
            botonHabilidad.GetComponent<Button>().interactable = true;
        }

        habilidadActiva = false;
    }
}
