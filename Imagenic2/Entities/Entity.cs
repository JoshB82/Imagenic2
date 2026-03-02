using Imagenic2.Core.Enums;

namespace Imagenic2.Core.Entities;

public abstract class Entity
{
    #region Fields and Properties

    private static int id;
    public int Id { get; private set; }

    internal event Action<RenderUpdate>? RenderAlteringPropertyChanged;

    #endregion

    #region Constructors

    protected Entity()
    {
        Id = id++;
    }

    #endregion

    #region Methods

    public virtual Entity ShallowCopy() => (Entity)MemberwiseClone();
    public virtual Entity DeepCopy()
    {
        var entity = ShallowCopy();
        entity.Id = id++;
        entity.RenderAlteringPropertyChanged = null;
        return entity;
    }

    internal void InvokeRenderEvent(RenderUpdate args)
    {
        RenderAlteringPropertyChanged?.Invoke(args);
    }

    #endregion
}