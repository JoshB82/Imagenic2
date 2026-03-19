using Imagenic2.Core.Entities;

namespace Imagenic2.Core.Utilities;

public partial class Node
{
    #region Fields and Properties

    public Entity? Content { get; set; }
    public Node? Parent { get; set; }
    public IList<Node> Children { get; set; } = new List<Node>();

    #endregion

    #region Constructors

    public Node()
    {
        
    }

    public Node(Entity? content) => Content = content;
    public Node(Entity? content, Node? parent) => (Content, Parent) = (content, parent);
    public Node(Entity? content, IList<Node> children) => (Content, Children) = (content, children);
    public Node(Entity? content, Node? parent, IList<Node> children) => (Content, Parent, Children) = (content, parent, children);

    #endregion

    #region Methods

    public void AddChildren(params IEnumerable<Entity> children)
    {
        foreach (Entity child in children)
        {
            Children.Add(new Node(child) { Parent = this });
        }
    }

    public void RemoveChildren(params IEnumerable<Entity> children)
    {
        List<Node> removed = new List<Node>();
        foreach (Entity child in children)
        {
            removed.AddRange(Children.Where(c => c.Content == child));
        }
        Children = Children.Where(c => !removed.Contains(c)).ToList();

        foreach (Node node in removed)
        {
            node.Parent = null;
        }
    }

    public IEnumerable<Node> GetAncestors(Func<Node, bool>? predicate = null)
    {
        Node? current = Parent;
        while (current != null)
        {
            if (predicate is null || predicate(current)) yield return current;
            current = current.Parent;
        }
    }

    public IEnumerable<Node> GetAncestorsAndThis(Func<Node, bool>? predicate = null)
    {
        yield return this;
        foreach (Node parent in GetAncestors(predicate))
        {
            yield return parent;
        }
    }

    public IEnumerable<Node> GetDescendants(Func<Node, bool>? predicate = null)
    {
        foreach (Node child in Children)
        {
            if (predicate is null || predicate(child)) yield return child;
            foreach (Node grandchild in child.GetDescendants())
            {
                yield return grandchild;
            }
        }
    }

    public IEnumerable<Node> GetDescendantsAndThis(Func<Node, bool>? predicate = null)
    {
        yield return this;
        foreach (Node child in GetDescendants(predicate))
        {
            yield return child;
        }
    }

    #region Transformations

    public Node TranslateX(float distanceX)
    {
        Vector3D displacement = new Vector3D(distanceX, 0, 0);
        return Translate(displacement);
    }

    public Node TranslateY(float distanceY)
    {
        Vector3D displacement = new Vector3D(0, distanceY, 0);
        return Translate(displacement);
    }

    public Node TranslateZ(float distanceZ)
    {
        Vector3D displacement = new Vector3D(0, 0, distanceZ);
        return Translate(displacement);
    }

    public Node Translate(float distanceX, float distanceY, float distanceZ)
    {
        Vector3D displacement = new Vector3D(distanceX, distanceY, distanceZ);
        return Translate(displacement);
    }

    public Node Translate(Vector3D displacement)
    {
        var descendants = GetDescendantsAndThis(n => n.Content is TranslatableEntity);
        foreach (Node node in descendants)
        {
            ((TranslatableEntity)(node.Content)).Translate(displacement);
        }

        return this;
    }

    #endregion

    #endregion
}