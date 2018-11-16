using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour {

    public CleanerEnemy2_0 herb;
    public Seeker shenzi;
    public Golem golem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (herb != null)
            {
                herb.enabled = true;
            }

            if (shenzi != null)
            {
                shenzi.enabled = true;
            }

            if (golem != null)
            {
                golem.enabled = true;
            }
        }
    }
}
