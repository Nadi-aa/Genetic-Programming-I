using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    public float r; // Red color genetic code
    public float g; // Green color genetic code
    public float b; // Blue color genetic code
    public bool isDead = false; // Tracks whether the object is dead
    public float timeToDie = 0; // Time of the object's death
    SpriteRenderer sRenderer; // Visual component
    Collider2D sCollider; // Collision component

    // Initializes components at the start
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        sRenderer.color = new Color(r, g, b); // Sets the object's color
    }

    // Kills the object when clicked
    void OnMouseDown()
    {
        isDead = true;
        timeToDie = PopulationManager_sc.elapsed; // Records the elapsed time
        sRenderer.enabled = false; // Disables the visual component
        sCollider.enabled = false; // Disables the collision component
        Debug.Log("Died at: " + timeToDie);
    }
}
