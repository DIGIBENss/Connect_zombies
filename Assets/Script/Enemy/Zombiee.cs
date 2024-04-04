
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Zombiee : MonoBehaviour
{
    public int Type;
    public int customID;
    private static int NextID = 0;

    public bool ShouldSpawn { get; internal set; }

    public void CustomId()
   {
        customID = NextID++;
        //print("ID" + customID);
   }

}
