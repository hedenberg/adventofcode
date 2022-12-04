namespace Bohl.AdventOfCode.Input;

public static class ArrayExtensions
{
    public static void Deconstruct<T>(this T[] srcArray, out T a0, out T a1)
    {
        if (srcArray == null || srcArray.Length < 2)
            throw new ArgumentException(nameof(srcArray));

        a0 = srcArray[0];
        a1 = srcArray[1];
    }
}