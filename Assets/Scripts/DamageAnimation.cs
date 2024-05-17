using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem damageParticle; //Aqui eu atrelei no inspector um prefab de particulas
    public ParticleSystem impactParticle;
    public  void PlayDamageAnimation(Transform target){
        Instantiate(damageParticle, target.position, Quaternion.identity);
    }

    public void PlayImpactAnimation(Transform target){
        Instantiate(impactParticle, target.position, Quaternion.identity);
    }

}
