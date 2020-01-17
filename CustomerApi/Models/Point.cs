using System;

/// C# 8.0 added the ability to mark a struct property or method as `readonly`
///
/// Add `readonly` to all properties and methods that do *not* modify the
/// struct. This will al
public struct Point
{
    public double X { get; set; }
    public double Y { get; set; }

    /// Use `readonly` to designate
    public readonly double Distance => Math.Sqrt((X * X) + (Y * Y));

    ///
    ///
    public readonly override string ToString()
    {
        return $"({X}, {Y} is {Distance} from the origin";
    }

}