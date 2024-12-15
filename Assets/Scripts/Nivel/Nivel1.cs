using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class Nivel1 : MonoBehaviour
{
    public GameObject alienPrefab;
    public Transform paredSuperior;
    public float intervaloGeneracion = 3f;
    public float rangoHorizontal = 8f;
    public Transform barrera;

    public int limiteColisiones = 3;
    private int colisionesActuales = 0;

    public TextMeshProUGUI mensajeTexto;

    private float tiempoProximoAlien;
    private bool nivelTerminado = false;

    void Start()
    {
        if (barrera == null)
        {
            Debug.LogError("La barrera no está asignada.");
        }

        tiempoProximoAlien = Time.time + intervaloGeneracion;

        if (mensajeTexto != null)
        {
            mensajeTexto.text = "";
        }
    }

    void Update()
    {
        if (!nivelTerminado)
        {
            if (Time.time >= tiempoProximoAlien)
            {
                GenerarAlien();
                tiempoProximoAlien = Time.time + intervaloGeneracion;
            }
        }
    }

    void GenerarAlien()
    {
        float centroPantalla = Camera.main.transform.position.x;
        float posicionX = Random.Range(-rangoHorizontal, rangoHorizontal) + centroPantalla;
        Vector2 posicionAlien = new Vector2(posicionX, paredSuperior.position.y);

        GameObject nuevoAlien = Instantiate(alienPrefab, posicionAlien, Quaternion.identity);

        LogicaAlienTipo1 logicaAlien = nuevoAlien.GetComponent<LogicaAlienTipo1>();
        if (logicaAlien != null)
        {
            logicaAlien.barrera = barrera;
            logicaAlien.OnBarreraColision += AlienTocoBarrera;
        }
    }

    void AlienTocoBarrera()
    {
        colisionesActuales++;
        Debug.Log("Colisiones con la barrera: " + colisionesActuales);

        if (colisionesActuales >= limiteColisiones)
        {
            TerminarNivel();
        }
    }

    void TerminarNivel()
    {
        nivelTerminado = true;

        if (mensajeTexto != null)
        {
            mensajeTexto.text = "¡Has perdido! Volviendo al menú...";
        }

        Time.timeScale = 0f;

        StartCoroutine(VolverAlMenu());
    }

    private System.Collections.IEnumerator VolverAlMenu()
    {
        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menu"); 
    }
}