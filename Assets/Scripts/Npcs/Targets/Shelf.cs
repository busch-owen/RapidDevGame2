using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [field: SerializeField] public ItemTypeSo AssignedItem { get; private set; }
}
