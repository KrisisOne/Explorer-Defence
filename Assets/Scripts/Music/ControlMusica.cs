using UnityEngine;
using UnityEngine.UI;

public class ControlMusica : MonoBehaviour
{
    public static ControlMusica instancia;
    public AudioSource audioSource;
    public Button botonMusica;
    private bool musicaEncendida = true;

    private void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
