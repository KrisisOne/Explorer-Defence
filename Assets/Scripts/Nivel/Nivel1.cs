using UnityEngine;

public class Nivel1 : MonoBehaviour
{
    public GameObject alienPrefab;
    public Transform paredSuperior;
    public float intervaloGeneracion = 2f;
    public float rangoHorizontal = 8f;
    public Transform barrera;


    private float tiempoProximoAlien;

    void Start() {

        if (barrera == null) {
            Debug.LogError("La barrera no está asignada al alien: " + gameObject.name);
        }

        tiempoProximoAlien = Time.time + intervaloGeneracion;
    }

    void Update() {
        
        if (Time.time >= tiempoProximoAlien) {
            
            GenerarAlien();
            tiempoProximoAlien = Time.time + intervaloGeneracion;

        }
    }

    void GenerarAlien() {
        float centroPantalla = Camera.main.transform.position.x;

        float posicionX = Random.Range(-rangoHorizontal, rangoHorizontal) + centroPantalla;
        Vector2 posicionAlien = new Vector2(posicionX, paredSuperior.position.y);

        GameObject nuevoAlien = Instantiate(alienPrefab, posicionAlien, Quaternion.identity);

        LogicaAlienTipo1 logicaAlien = nuevoAlien.GetComponent<LogicaAlienTipo1>();

        Debug.Log("Alien creado.");

        if (logicaAlien != null){

            logicaAlien.barrera = barrera;
            Debug.Log("Barrera asignada al alien generado.");

        } else {

            Debug.LogError("El alien instanciado no tiene el componente LogicaAlienTipo1.");
    
        }
    }
}

