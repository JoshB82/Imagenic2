using System.Drawing;

namespace Imagenic2.Core.Entities.Lights;

internal static class ColourBlending
{
    public static Color Mix(this Color colour, Color mixingColour)
    {
        int newA = (0.5f * (colour.A + mixingColour.A)).RoundToInt();
        int newR = (0.5f * (colour.R + mixingColour.R)).RoundToInt();
        int newG = (0.5f * (colour.G + mixingColour.G)).RoundToInt();
        int newB = (0.5f * (colour.B + mixingColour.B)).RoundToInt();

        return Color.FromArgb(newA, newR, newG, newB);
    }
}
