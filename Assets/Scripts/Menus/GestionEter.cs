using UnityEngine;
using TMPro;

public class GestionarEter : MonoBehaviour
{
    public int cantidadEter = 0;
    public TextMeshProUGUI textoEter;

    void Start()
    {
        CargarEter();
        ActualizarTextoEter();
    }

    public void SumarEter(int cantidad)
    {
        cantidadEter += cantidad;
        GuardarEter();
        ActualizarTextoEter();
    }

    private void ActualizarTextoEter()
    {
        if (textoEter != null)
        {
            textoEter.text = $"{cantidadEter}";
        }
    }

    public void GuardarEter()
    {
        PlayerPrefs.SetInt("Eter", cantidadEter);
        PlayerPrefs.Save();
    }

    public void CargarEter()
    {
        cantidadEter = PlayerPrefs.GetInt("Eter", 0);
    }
}
