using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMasterData<T>
{
    List<T> Get();
}

