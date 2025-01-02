using UnityEngine;
using TMPro;

public class GestionSkills : MonoBehaviour
{
    public int nivelHielo = 1;
    public int nivelMunicion = 1;
    public int nivelBomba = 1;
    public int nivelAtkSpeed = 1;
    public int nivelDaño = 1;
    public int costoHielo = 1000;
    public int costoMunicion = 1500;
    public int costoBomba = 2000;
    public int costoAtkSpeed = 1200;
    public int costoDaño = 1800;

    public TextMeshProUGUI textoNivelHielo;
    public TextMeshProUGUI textoNivelMunicion;
    public TextMeshProUGUI textoNivelBomba;
    public TextMeshProUGUI textoNivelAtkSpeed;
    public TextMeshProUGUI textoNivelDaño;

    public TextMeshProUGUI textoCostoHielo;
    public TextMeshProUGUI textoCostoMunicion;
    public TextMeshProUGUI textoCostoBomba;
    public TextMeshProUGUI textoCostoAtkSpeed;
    public TextMeshProUGUI textoCostoDaño;

    private GestionarEter gestionarEter;

    void Start()
    {
        gestionarEter = FindObjectOfType<GestionarEter>();
        CargarDatos();
        ActualizarTextos();
    }

    public void MejorarHabilidad(string habilidad)
    {
        switch (habilidad)
        {
            case "Hielo":
                MejorarNivel(ref nivelHielo, ref costoHielo, textoNivelHielo, textoCostoHielo, 200);
                break;

            case "Municion":
                MejorarNivel(ref nivelMunicion, ref costoMunicion, textoNivelMunicion, textoCostoMunicion, 300);
                break;

            case "Bomba":
                MejorarNivel(ref nivelBomba, ref costoBomba, textoNivelBomba, textoCostoBomba, 500);
                break;

            case "AtkSpeed":
                MejorarNivel(ref nivelAtkSpeed, ref costoAtkSpeed, textoNivelAtkSpeed, textoCostoAtkSpeed, 250);
                break;

            case "Daño":
                MejorarNivel(ref nivelDaño, ref costoDaño, textoNivelDaño, textoCostoDaño, 400);
                break;

            default:
                Debug.LogError($"Habilidad '{habilidad}' no encontrada.");
                break;
        }

        GuardarDatos();
        ActualizarTextos();
    }

    private void MejorarNivel(ref int nivel, ref int costo, TextMeshProUGUI textoNivel, TextMeshProUGUI textoCosto, int incrementoCosto)
    {
        
        if (gestionarEter.cantidadEter >= costo)
        {
            gestionarEter.SumarEter(-costo);
            nivel++;
            costo += incrementoCosto; 

            if (textoNivel != null) textoNivel.text = $"Nivel: {nivel}";
            if (textoCosto != null) textoCosto.text = $"Costo: {costo}";

        }

    }

    private void ActualizarTextos()
    {
        if (textoNivelHielo != null) textoNivelHielo.text = $"{nivelHielo}";
        if (textoNivelMunicion != null) textoNivelMunicion.text = $"{nivelMunicion}";
        if (textoNivelBomba != null) textoNivelBomba.text = $"{nivelBomba}";
        if (textoNivelAtkSpeed != null) textoNivelAtkSpeed.text = $"{nivelAtkSpeed}";
        if (textoNivelDaño != null) textoNivelDaño.text = $"{nivelDaño}";

        if (textoCostoHielo != null) textoCostoHielo.text = $"{costoHielo}";
        if (textoCostoMunicion != null) textoCostoMunicion.text = $"{costoMunicion}";
        if (textoCostoBomba != null) textoCostoBomba.text = $"{costoBomba}";
        if (textoCostoAtkSpeed != null) textoCostoAtkSpeed.text = $"{costoAtkSpeed}";
        if (textoCostoDaño != null) textoCostoDaño.text = $"{costoDaño}";
    }

    private void GuardarDatos()
    {
        PlayerPrefs.SetInt("NivelHielo", nivelHielo);
        PlayerPrefs.SetInt("CostoHielo", costoHielo);

        PlayerPrefs.SetInt("NivelMunicion", nivelMunicion);
        PlayerPrefs.SetInt("CostoMunicion", costoMunicion);

        PlayerPrefs.SetInt("NivelBomba", nivelBomba);
        PlayerPrefs.SetInt("CostoBomba", costoBomba);

        PlayerPrefs.SetInt("NivelAtkSpeed", nivelAtkSpeed);
        PlayerPrefs.SetInt("CostoAtkSpeed", costoAtkSpeed);

        PlayerPrefs.SetInt("NivelDaño", nivelDaño);
        PlayerPrefs.SetInt("CostoDaño", costoDaño);

        PlayerPrefs.Save();
    }

    private void CargarDatos()
    {
        nivelHielo = PlayerPrefs.GetInt("NivelHielo", 1);
        costoHielo = PlayerPrefs.GetInt("CostoHielo", 1000);

        nivelMunicion = PlayerPrefs.GetInt("NivelMunicion", 1);
        costoMunicion = PlayerPrefs.GetInt("CostoMunicion", 1500);

        nivelBomba = PlayerPrefs.GetInt("NivelBomba", 1);
        costoBomba = PlayerPrefs.GetInt("CostoBomba", 2000);

        nivelAtkSpeed = PlayerPrefs.GetInt("NivelAtkSpeed", 1);
        costoAtkSpeed = PlayerPrefs.GetInt("CostoAtkSpeed", 1200);

        nivelDaño = PlayerPrefs.GetInt("NivelDaño", 1);
        costoDaño = PlayerPrefs.GetInt("CostoDaño", 1800);
    }
}
