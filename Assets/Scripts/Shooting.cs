using UnityEngine;

public class Shooting : ClickSpawner
{
    [SerializeField]
    [Tooltip("How many points to add to the shooter, if the laser hits its target")]
    int pointsToAdd = 1;

    protected override GameObject spawnObject()
    {
        GameObject newObject = base.spawnObject();  // base = super

        return newObject;
    }
}
