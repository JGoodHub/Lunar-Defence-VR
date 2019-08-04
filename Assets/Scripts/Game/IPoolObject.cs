using System;
using System.Collections.Generic;
using UnityEngine;

interface IPoolObject {
    void ActivateObject ();
    void PassivateObject ();

    //TODO ---> Create a dedicated class for storing and serving up pooled objects
    //          Dictionaries of Lists could be used to add new pools and retrieve object by category
}