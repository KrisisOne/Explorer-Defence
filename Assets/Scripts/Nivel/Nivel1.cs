using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Nivel1 : MonoBehaviour
{
    public GameObject alienPrefab;
    public Transform paredSuperior;
    public float intervaloGeneracion = 3f;
    public float rangoHorizontal = 8f;
    public Transform barrera;

    public int limiteColisiones = 3;
    private int colisionesActuales = 0;

    public TextMeshProUGUI textoTiempo;

    public Image imagenGanar;
    public Image imagenPerder;

    private float tiempoProximoAlien;
    private bool nivelTerminado = false;

    public float tiempoRestante = 60f;

    public GameObject imagenTresVidas;
    public GameObject imagenDosVidas;
    public GameObject imagenUnaVida;

    private GestionarEter gestionarEter;

    void Start()
    {
        gestionarEter = FindObjectOfType<GestionarEter>();

        tiempoProximoAlien = Time.time + intervaloGeneracion;

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
        Debug.LogError("entro en derrota");
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
