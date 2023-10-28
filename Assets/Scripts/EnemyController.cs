using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private AIDestinationSetter destinationSetter;

    private void Start()
    {
        // Encuentra el GameObject del jugador con la etiqueta "Player".
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            // Obtén el componente AIDestinationSetter de este GameObject.
            destinationSetter = GetComponent<AIDestinationSetter>();

            if (destinationSetter != null)
            {
                // Asigna el transform del jugador como destino.
                destinationSetter.target = playerObject.transform;
            }
            else
            {
                Debug.LogError("AIDestinationSetter no encontrado en el enemigo.");
            }
        }
        else
        {
            Debug.LogError("Jugador (Player) no encontrado con la etiqueta 'Player'.");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GameController gameManager = FindFirstObjectByType<GameController>();
            gameManager.IncrementKillCount();

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
