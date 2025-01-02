using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Nivel2 : MonoBehaviour
{
    public GameObject alienTipo1Prefab;
    public GameObject alienTipo2Prefab;
    public Transform paredSuperior;
    public float intervaloGeneracionAlienTipo1 = 3f;
    public float intervaloGeneracionAlienTipo2 = 5f;
    public float rangoHorizontal = 8f;
    public Transform barrera;

    public int limiteColisiones = 3;
    private int colisionesActuales = 0;
    public TextMeshProUGUI textoTiempo;

    public Image imagenGanar;
    public Image imagenPerder;

    private float tiempoProximoAlienTipo1;
    private float tiempoProximoAlienTipo2;
    private bool nivelTerminado = false;

    public float tiempoRestante = 60f;

    public GameObject imagenTresVidas;
    public GameObject imagenDosVidas;
    public GameObject imagenUnaVida;
    private GestionarEter gestionarEter;

    void Start()
    {
        gestionarEter = FindObjectOfType<GestionarEter>();

        tiempoProximoAlienTipo1 = Time.time + intervaloGeneracionAlienTipo1;
        tiempoProximoAlienTipo2 = Time.time + intervaloGeneracionAlienTipo2;

        if (textoTiempo != null)
        {
            ActualizarTextoTiempo();
        }

        if (imagenGanar != null)
        {
            imagenGanar.gameObject.SetActive(false);
        }

        if (imagenPerder != null)
        {
            imagenPerder.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!nivelTerminado)
        {
            ActualizarTiempo();

            if (Time.time >= tiempoProximoAlienTipo1)
            {
                GenerarAlien(alienTipo1Prefab);
                tiempoProximoAlienTipo1 = Time.time + intervaloGeneracionAlienTipo1;
            }

            if (Time.time >= tiempoProximoAlienTipo2)
            {
                GenerarAlien(alienTipo2Prefab);
                tiempoProximoAlienTipo2 = Time.time + intervaloGeneracionAlienTipo2;
            }
        }
    }

    void GenerarAlien(GameObject alienPrefab)
    {
        float centroPantalla = Camera.main.transform.position.x;
        float posicionX = Random.Range(-rangoHorizontal, rangoHorizontal) + centroPantalla;
        Vector2 posicionAlien = new Vector2(posicionX, paredSuperior.position.y);

        GameObject nuevoAlien = Instantiate(alienPrefab, posicionAlien, Quaternion.identity);

        if (alienPrefab.GetComponent<LogicaAlienTipo1>() != null)
        {
            LogicaAlienTipo1 logicaAlien = nuevoAlien.GetComponent<LogicaAlienTipo1>();
            if (logicaAlien != null)
            {
                logicaAlien.barrera = barrera;
                logicaAlien.vida += 100;
                logicaAlien.OnBarreraColision += AlienTocoBarrera;
            }
        }

        if (alienPrefab.GetComponent<LogicaAlienTipo2>() != null)
        {
            LogicaAlienTipo2 logicaAlien = nuevoAlien.GetComponent<LogicaAlienTipo2>();
            if (logicaAlien != null)
            {
                logicaAlien.barrera = barrera;
                logicaAlien.OnBarreraColision += AlienTocoBarrera;
            }
        }
    }


    void AlienTocoBarrera()
    {
        colisionesActuales++;
        ActualizarVidas();

        if (colisionesActuales >= limiteColisiones)
        {
            PerderNivel();
        }
    }

    private void ActualizarVidas()
    {
        if (colisionesActuales == 0)
        {
            imagenTresVidas.SetActive(true);
            imagenDosVidas.SetActive(false);
            imagenUnaVida.SetActive(false);
        }
        else if (colisionesActuales == 1)
        {
            imagenTresVidas.SetActive(false);
            imagenDosVidas.SetActive(true);
            imagenUnaVida.SetActive(false);
        }
        else if (colisionesActuales == 2)
        {
            imagenTresVidas.SetActive(false);
            imagenDosVidas.SetActive(false);
            imagenUnaVida.SetActive(true);
        }
    }

    void ActualizarTiempo()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTextoTiempo();
        }
        else
        {
            tiempoRestante = 0;
            if (!nivelTerminado)
            {
                GanarNivel();
            }
        }
    }

    void ActualizarTextoTiempo()
    {
        int minutos = Mathf.FloorToInt(tiempoRestante / 60);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60);
        textoTiempo.text = $"{minutos}:{segundos:D2}";
    }

    void GanarNivel()
    {
        nivelTerminado = true;

        if (imagenGanar != null)
        {
            imagenGanar.gameObject.SetActive(true);
        }
        if (gestionarEter != null)
        {
            gestionarEter.SumarEter(200);
        }

        Time.timeScale = 0f;

        StartCoroutine(VolverAlMenu());
    }

    void PerderNivel()
    {
        nivelTerminado = true;

        if (imagenPerder != null)
        {
            imagenPerder.gameObject.SetActive(true);
        }

        Time.timeScale = 0f;

        StartCoroutine(VolverAlMenu());
    }

    private System.Collections.IEnumerator VolverAlMenu()
    {
        yield return new WaitForSecondsRealtime(4f);

        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
