namespace Imagenic2.Core.Entities;

public abstract class Entity
{
    #region Fields and Properties

    private static int id;
    public int Id => id++;

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

    #endregion
}