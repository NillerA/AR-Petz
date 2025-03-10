using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public static class PlayerManager
{

    public static int food = 5;
    public enum aniamls
    {
        Cat,
        Dog,
        Mouse,
        Parrot,
        Turtoise,
        Rabbit,
        Goldfish
    }
    public static aniamls chosenAnimal = aniamls.Dog;

    public static animalInfo animalInfo = new animalInfo();

    //public static IEnumerator()
    //{
    //    yield return new WaitForSeconds(1);
    //}
}

public class animalInfo
{
    public int Hunger
    {
        get
        {
            return Hunger;
        }
        set
        {
            Hunger = math.clamp(value, 0, 100);
        }
    }
    public int Hapiness 
    {
        get
        {
            return Hapiness;
        }
        set 
        {
            Hapiness = math.clamp(value, 0, 100);
        }
    }
}