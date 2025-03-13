using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public static class PlayerManager
{

    public static int food = 5;
    public enum animals
    {
        Cat,
        Dog,
        Mouse,
        Parrot,
        Turtoise,
        Rabbit,
        Goldfish
    }
    public static animals chosenAnimal = animals.Cat;

    public static animalInfo animalInfo = new animalInfo();
    public static GameObject animal;

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