using UnityEngine;

public class LogicaDisparos : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D colision)
    {

        if (colision.gameObject.CompareTag("Pared")){

            Destroy(gameObject);

        }
    }
}
