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
    public static aniamls chosenAnimal = aniamls.Cat;

}