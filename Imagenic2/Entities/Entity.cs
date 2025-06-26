using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public abstract class Entity
{
    #region Fields and Properties

    private static int id;
    public int Id => id++;

    internal event Action<RenderUpdate>? RenderAlteringPropertyChanged;

    #endregion

    #region Constructors

    protected Entity()
    {

    }

    #endregion

    #region Methods

    public virtual Entity ShallowCopy() => (Entity)MemberwiseClone();
    public virtual Entity DeepCopy()
    {
        var entity = ShallowCopy();
        return entity;
    }

    internal void InvokeRenderEvent(RenderUpdate args)
    {
        RenderAlteringPropertyChanged?.Invoke(args);
    }

    #endregion
}