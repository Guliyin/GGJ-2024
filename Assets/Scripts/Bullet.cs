using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected bool firstTouch = true;

    protected void Destroy()
    {
        Destroy(gameObject);
    }

    protected virtual void Touch()
    {
        if (firstTouch)
        {
            firstTouch = false;
            EventCenter.Broadcast(FunctionType.Touch);
        }
    }
}
