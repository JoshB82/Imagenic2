using Imagenic2.Core.Images;
using Imagenic2.Core.Renderers.Rasterising;

namespace Imagenic2.Core.Renderers;

public sealed class Buffer2D<T>
{
    #region Fields and Properties

    private int width, height;

    public int Width
    {
        get => width;
        set
        {
            width = value;
            //SetSizes(firstDimensionSize, secondDimensionSize);
        }
    }

    public int Height
    {
        get => height;
        set
        {
            height = value;
            //SetSizes(firstDimensionSize, secondDimensionSize);
        }
    }

    /*
    private void SetSizes(int firstDimensionSize, int secondDimensionSize)
    {
        Values = new T[firstDimensionSize][];
        for (int i = 0; i < firstDimensionSize; i++)
        {
            Values[i] = new T[secondDimensionSize];
        }
    }
    */

    public T?[] Values { get; private set; }

    public T? this[int x, int y]
    {
        get => Values[x * height + y];
        set => Values[x * height + y] = value;
    }

    /*
    public T?[][] Values { get; set; }

    internal T? this[int x, int y]
    {
        get => Values[x][y];
        set => Values[x][y] = value;
    }
    */

    #endregion

    #region Constructors

    public Buffer2D(int width, int height)
    {
        this.width = width;
        this.height = height;
        Values = new T?[width * height];
        //SetSizes(firstDimensionSize, secondDimensionSize);
    }

    #endregion

    #region Methods

    public Buffer2D<T> DeepCopy()
    {
        Buffer2D<T> newBuffer2D = new Buffer2D<T>(width, height);
        newBuffer2D.ForEach((t, i, j) => t = this[i, j]);
        return newBuffer2D;
    }

    public void SetAllToValue(T? value)
    {
        /*for (int i = 0; i < firstDimensionSize; i++)
        {
            for (int j = 0; j < secondDimensionSize; j++)
            {
                this[i, j] = value;
            }
        }*/

        Values.AsSpan().Fill(value);
    }

    public void SetAllToDefault() => SetAllToValue(default);

    public void ForEach(Action<T?> action)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                action(this[i, j]);
            }
        }
    }

    public void ForEach(Action<T?, int, int> action)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                action(this[i, j], i, j);
            }
        }
    }

    public void ParallelForEach(Action<T?> action)
    {
        Parallel.For(0, width * height, i =>
        {
            T? value = this[i % width, i / width];
            action(value);
        });
    }

    //public static explicit operator T[](Buffer2D<T> buffer)
    //{
        //T[] array = new T[buffer.firstDimensionSize * buffer.secondDimensionSize];
        //buffer.ForEach((t, i, j) => array[i * buffer.firstDimensionSize + j] = buffer.Values[i][j]);
        //return array;
    //}
    
    public TImage ToImage<TImage>() where TImage : Imagenic2.Core.Images.Image, IFactory<TImage>
    {
        Buffer2D<Colour> imageValues = null;
        if (typeof(T) == typeof(Colour))
        {
            imageValues = this as Buffer2D<Colour>;
        }
        else if (typeof(T) == typeof(float))
        {
            imageValues = new Buffer2D<Colour>(width, height);
            imageValues.ForEach((colour, x, y) =>
            {
                float value = (float)((object)(this[x, y]));
                int scaledDepth = value == Rasteriser<TImage>.backgroundValue ? 255 : ((value + 1) * 255 / 2).RoundToInt();
                //imageValues.Values[x][y] = Color.FromArgb(scaledDepth, scaledDepth, scaledDepth);
            });
        }
        else
        {
            throw new Exception();
        }

        /*
        if (typeof(TImage) == typeof(Imagenic2.Core.Images.Bitmap))
        {
            return new Imagenic2.Core.Images.Bitmap(imageValues) as TImage;
        }
        else
        {
            throw new Exception();
        }
        */

        return TImage.CreateFromBuffer(imageValues);
    }

    #endregion
}