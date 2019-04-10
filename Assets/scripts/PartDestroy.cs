using UnityEngine;
using System.Collections;

/*Small script written to handle the deletion of particle systems
 * written to handle the constant instantion of particles
 * maintaining a a good framerate and performance for this game on the Hololens was top priority for game play
 * So destroying all unnecessary objects was crucial
 */
public class PartDestroy : MonoBehaviour
{
    private ParticleSystem ps;


    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}