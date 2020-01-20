using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static bool canInteract = true;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        canInteract = true;
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {

            if (!canInteract) return;

            Vector2 origin = transform.position;
            Vector2 direction = playerInput.GetDirection();
            float distance = 1f;
            int layerMask = LayerMask.GetMask("NPC", "Interactable"); 

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance,layerMask);
            Debug.DrawRay(origin, direction, Color.blue, distance);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.OnInteractable();
                }
            }
        }
    }
}
