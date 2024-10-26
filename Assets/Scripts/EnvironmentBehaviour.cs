using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBehaviour : MonoBehaviour
{
    public enum ObjectType
    {
        None, Ground, Building, Glass, Plant, Rock, Wood, Metal
    }

    public ObjectType objectType;

    public AudioClip GroundSound;
    public AudioClip BuildingSound;
    public AudioClip GlassSound;
    public AudioClip PlantSound;
    public AudioClip RockSound;
    public AudioClip WoodSound;
    public AudioClip MetalSound;

    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        switch(objectType)
        {
            case ObjectType.None: 
                break;
            case ObjectType.Ground:
                GetComponent<AudioSource>().clip = GroundSound;
                break;
            case ObjectType.Building:
                GetComponent<AudioSource>().clip = BuildingSound;
                break;
            case ObjectType.Glass:
                GetComponent<AudioSource>().clip = GlassSound;
                break;
            case ObjectType.Plant:
                GetComponent<AudioSource>().clip = PlantSound;
                break;
            case ObjectType.Rock:
                GetComponent<AudioSource>().clip = RockSound;
                break;
            case ObjectType.Wood:
                GetComponent<AudioSource>().clip = WoodSound;
                break;
            case ObjectType.Metal:
                GetComponent<AudioSource>().clip = MetalSound;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
