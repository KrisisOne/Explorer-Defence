using UnityEngine;

public class LogicaBlaster : MonoBehaviour
{
    public GameObject disparos;
    public Transform cañon;
    public float atkSpeed = 2f;
    public float cooldownDisparos = 0f;

    void Update(){

        if(Time.time >= cooldownDisparos){
            Shoot();
            cooldownDisparos = Time.time + 1f / atkSpeed;
        }


    }

    void Shoot() {

        if (cañon != null) {

            GameObject proyectil = Instantiate(disparos, cañon.position, Quaternion.identity);
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();

            if (rb != null) {

                rb.linearVelocity = new Vector2(0f, 10f); 

            }

        }

    }

}
