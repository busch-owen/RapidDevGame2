using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "My Assets/NpcTypeSO")]

public class NpcTypeSo : ScriptableObject
{
    [field:SerializeField]public List<String> PositiveText{ get; private set; }
    
    [field:SerializeField]public List<String> NegativeText{ get; private set; }
    
    [field:SerializeField]public List<String> OpeningText{ get; private set; }
    
    [field:SerializeField]public List<ItemTypeSo> PossibleItems{ get; private set; }
    [field: SerializeField] public float OddsToSpawn { get; private set; }
    [field:SerializeField]public int Speed{ get; private set; }
    
    [field:SerializeField]public int Budget{ get; private set; }
    
    [field:SerializeField]public int ShelvesTillExit{ get; private set; }
    
    [field:SerializeField]public bool ShopLifter{ get; private set; }
    
    [field:SerializeField]public Sprite NpcSprite{ get; private set; }
    
    [field:SerializeField]public List<Image> Bad{ get; private set; }
    
    [field:SerializeField]public List<Image> Good{ get; private set; }
    
    [field:SerializeField]public List<Image> Opening{ get; private set; }
    
    [field:SerializeField]public List<Sprite> PossibleBad{ get; private set; }
    
    [field:SerializeField]public List<Sprite> PossibleGood{ get;  private set; }
    
    
    [field:SerializeField]public Sprite PossibleOpening{ get; private set; }
    
    
    
    
    
}
