namespace ConsoleAdventure;

public class TreeNode
{
    public TreeNode Parent
    {
        get; private set;
    }

    public TreeNode LeftChild
    {
        get; private set;
    }

    public TreeNode RightChild
    {
        get; private set;
    }

    public int Value
    {
        get; private set;
    }

    public TreeNode(int value, TreeNode parent)
    {
        Value = value;
        Parent = parent;
    }

    public void AddChild(int newValue) {
        if (newValue < Value)
        {
            if (LeftChild == null)
                LeftChild = new TreeNode(newValue, this);
            else
                LeftChild.AddChild(newValue);
        } 
        else if (newValue > Value)
        {
            if (RightChild == null)
                RightChild = new TreeNode(newValue, this);
            else
                RightChild.AddChild(newValue);
        }
        else
        {
            Console.WriteLine("Doublette ignored: " + newValue);
        }
    }


    public override string ToString() => Level() + ":" + Value + " ";

    public string Inorder() {
        string order = "";
        if (LeftChild != null){
            order += LeftChild.Inorder();
        }
        order += ToString();
        if (RightChild != null)
        {
            order += RightChild.Inorder();
        }
        return order;
    }

    public string Preorder()
    {
        string order = "";
        order += ToString();
        if (LeftChild != null)
        {
            order += LeftChild.Preorder();
        }
        if (RightChild != null)
        {
            order += RightChild.Preorder();
        }
        return order;
    }

    public string Postorder()
    {
        string order = "";
        if (LeftChild != null)
        {
            order += LeftChild.Postorder();
        }
        if (RightChild != null)
        {
            order += RightChild.Postorder();
        }
        order += ToString();
        return order;
    }

    public string Levelorder()
    {
        Queue<TreeNode> q = new Queue<TreeNode>();
        q.Enqueue(this);
        string order = "";

        TreeNode currentNode;
        while(q.Count > 0) {
            currentNode = q.Dequeue();
            order += currentNode.ToString();
            if (currentNode.LeftChild != null) {
                q.Enqueue(currentNode.LeftChild);
            }
            if (currentNode.RightChild != null)
            {
                q.Enqueue(currentNode.RightChild);
            }
        }
        return order;
    }


    public int Level()
    {
        return (Parent == null) ? 0 : Parent.Level() + 1;
    }

    public int Height()
    {
        if (LeftChild == null && RightChild == null)
        {
            return 0;
        }
        int lh = 0;
        int rh = 0;
        if (LeftChild != null)
        {
            lh = LeftChild.Height();
        }
        if (RightChild != null)
        {
            rh = RightChild.Height();
        }
        return 1 + Math.Max(lh, rh);
    }


    public void PrintNode()
    {
        string line = "";
        for (int i=0; i<Level(); i++)
        {
            line += "|  ";
        }
        line += "+--";
        line += ToString();
        Console.WriteLine(line);
        if (LeftChild != null)
        {
            LeftChild.PrintNode();
        }
        if (RightChild != null)
        {
            RightChild.PrintNode();
        }
    }
}