using Imagenic2.Core.Renderers.Rasterising;
using System.Drawing;

namespace Imagenic2.Core.Renderers;

public sealed class Buffer2D<T>
{
    #region Fields and Properties

    private int firstDimensionSize, secondDimensionSize;

    public int FirstDimensionSize
    {
        get => firstDimensionSize;
        set
        {
            firstDimensionSize = value;
            SetSizes(firstDimensionSize, secondDimensionSize);
        }
    }

    public int SecondDimensionSize
    {
        get => secondDimensionSize;
        set
        {
            secondDimensionSize = value;
            SetSizes(firstDimensionSize, secondDimensionSize);
        }
    }

    private void SetSizes(int firstDimensionSize, int secondDimensionSize)
    {
        Values = new T[firstDimensionSize][];
        for (int i = 0; i < firstDimensionSize; i++)
        {
            Values[i] = new T[secondDimensionSize];
        }
    }

    public T?[][] Values { get; set; }

    #endregion

    #region Constructors

    public Buffer2D(int firstDimensionSize, int secondDimensionSize)
    {
        this.firstDimensionSize = firstDimensionSize;
        this.secondDimensionSize = secondDimensionSize;
        SetSizes(firstDimensionSize, secondDimensionSize);
    }

    #endregion

    #region Methods

    public void SetAllToValue(T? value)
    {
        for (int i = 0; i < firstDimensionSize; i++)
        {
            for (int j = 0; j < secondDimensionSize; j++)
            {
                Values[i][j] = value;
            }
        }
    }

    public void SetAllToDefault() => SetAllToValue(default);

    public void ForEach(Action<T?> action)
    {
        for (int i = 0; i < firstDimensionSize; i++)
        {
            for (int j = 0; j < secondDimensionSize; j++)
            {
                action(Values[i][j]);
            }
        }
    }

    public void ForEach(Action<T?, int, int> action)
    {
        for (int i = 0; i < firstDimensionSize; i++)
        {
            for (int j = 0; j < secondDimensionSize; j++)
            {
                action(Values[i][j], i, j);
            }
        }
    }

    public static explicit operator T[](Buffer2D<T> buffer)
    {
        T[] array = new T[buffer.firstDimensionSize * buffer.secondDimensionSize];
        buffer.ForEach((t, i, j) => array[i * buffer.firstDimensionSize + j] = buffer.Values[i][j]);
        return array;
    }

    public TImage ToImage<TImage>() where TImage : Imagenic2.Core.Images.Image
    {
        Buffer2D<Color> imageValues = null;
        if (typeof(T) == typeof(Color))
        {
            imageValues = this as Buffer2D<Color>;
        }
        else if (typeof(T) == typeof(float))
        {
            imageValues = new Buffer2D<Color>(firstDimensionSize, secondDimensionSize);
            imageValues.ForEach((colour, x, y) =>
            {
                float value = (float)((object)(Values[x][y]));
                int scaledDepth = value == Rasteriser<TImage>.backgroundValue ? 255 : ((value + 1) * 255 / 2).RoundToInt();
                imageValues.Values[x][y] = Color.FromArgb(scaledDepth, scaledDepth, scaledDepth);
            });
        }
        else
        {
            throw new Exception();
        }

        if (typeof(TImage) == typeof(Imagenic2.Core.Images.Bitmap))
        {
            return new Imagenic2.Core.Images.Bitmap(imageValues) as TImage;
        }
        else
        {
            throw new Exception();
        }
    }

    #endregion
}