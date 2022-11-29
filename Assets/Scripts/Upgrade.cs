using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public string Name;
    public Action Upgrad;

    public Upgrade(string name, Action upgrad)
    {
        Name = name;
        Upgrad = upgrad;
    }
}
