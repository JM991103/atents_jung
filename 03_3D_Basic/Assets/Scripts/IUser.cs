using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IUser
{
    Action OnObjectUse { get; set; }    // Action타입의 프로퍼티
}
