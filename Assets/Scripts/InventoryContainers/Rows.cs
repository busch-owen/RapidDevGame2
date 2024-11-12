using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Rows : MonoBehaviour
{

    public Transform Transform;

    public int index;

    public GameContainer Container;

    public Button button;

    public Shelf shelf;

    [SerializeField]private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
        Transform = this.transform;
        transform.localScale = new Vector3(0, 0, 0);
        shelf = GetComponentInParent<Shelf>();
        button = GetComponentInParent<Button>();
        button.onClick.AddListener(SetReference);
        button.onClick.AddListener(delegate{SoundManager.PlayClip(_clip);});
    }

    public void SetReference()
    {
        shelf.AssignRows(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
