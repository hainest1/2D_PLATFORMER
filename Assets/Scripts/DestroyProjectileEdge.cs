using UnityEngine;
using System.Collections;

public class DestroyProjectileEdge : MonoBehaviour {

    // Ideally, this can be attached to any object which will allow it to destroy a projectile upon impact.
    // For example, use on non-destructible walls.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
            Destroy(other.gameObject);
    }
}
