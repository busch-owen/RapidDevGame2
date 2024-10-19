using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/NpcTypeSO")]

public class NpcTypeSo : ScriptableObject
{
    [field:SerializeField]public List<String> PositiveText{ get; private set; }
    
    [field:SerializeField]public List<String> NegativeText{ get; private set; }
    
    [field:SerializeField]public List<String> OpeningText{ get; private set; }
    
    [field:SerializeField]public List<ItemTypeSo> Items{ get; private set; }
    
    [field:SerializeField]public int Speed{ get; private set; }
    
    [field:SerializeField]public int Budget{ get; private set; }
    
    [field:SerializeField]public Color Color{ get; private set; }
    
    [field:SerializeField]public int ShelvesTillExit{ get; private set; }
}
