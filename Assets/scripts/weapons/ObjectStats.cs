using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStats : MonoBehaviour
{
     public bool hover;
    public bool blueTeam;

    public void setHoversTrue()
    {
        hover = true;
    }

    public void setHoversFalse()
    {
        hover = false;
    }

    void Update()
    {
        // set hover to the coresponding team

    }

}
