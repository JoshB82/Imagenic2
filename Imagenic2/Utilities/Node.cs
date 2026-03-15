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

    #region Transformations

    public Node TranslateX(float distanceX)
    {
        if (Content is TranslatableEntity translatableEntityNode)
        {
            translatableEntityNode.TranslateX(distanceX);
        }
        var descendants = GetDescendants(n => n.Content is TranslatableEntity);
        foreach (Node node in descendants)
        {
            ((TranslatableEntity?)(node.Content)).TranslateX(distanceX);
        }

        return this;
    }

    #endregion

    #endregion
}