namespace Imagenic2.Entities;

public abstract class Entity
{
    private int id;
    public int Id => ++id;
}