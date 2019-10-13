using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InnovatedCollision : MonoBehaviour
{
    //Every movement feeds back the tiles that are currently occupied by the playerpiece
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Setup.currenttiles.Add(collision.gameObject);   
    }
}
