using UnityEngine;
using System.Collections;

public class ThunderStartTrigger : MonoBehaviour {

    public bool Played = false;
    public ThunderEffect MyThunderEffect;

    void OnTriggerEnter(Collider _coll)
    {
        if(!Played)
        {
            Played = true;
            MyThunderEffect.StartThunder();
        }
    }
}
