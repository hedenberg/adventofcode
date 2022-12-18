using System.Drawing;

namespace Bohl.AdventOfCode.Day12;

public interface IPathNode<TUserContext>
{
    bool IsWalkable(IPathNode<TUserContext> fromNode, TUserContext inContext);
}

public interface IIndexedObject
{
    int Index { get; set; }
}

public class SpatialAStar<TPathNode, TUserContext> where TPathNode : IPathNode<TUserContext>
{
    private static readonly double SQRT_2 = Math.Sqrt(2);
    private readonly PathNode[,] m_CameFrom;
    private readonly OpenCloseMap m_ClosedSet;
    private readonly OpenCloseMap m_OpenSet;
    private readonly PriorityQueue<PathNode> m_OrderedOpenSet;
    private readonly OpenCloseMap m_RuntimeGrid;
    private readonly PathNode[,] m_SearchSpace;

    public SpatialAStar(TPathNode[,] inGrid)
    {
        SearchSpace = inGrid;
        Width = inGrid.GetLength(0);
        Height = inGrid.GetLength(1);
        m_SearchSpace = new PathNode[Width, Height];
        m_ClosedSet = new OpenCloseMap(Width, Height);
        m_OpenSet = new OpenCloseMap(Width, Height);
        m_CameFrom = new PathNode[Width, Height];
        m_RuntimeGrid = new OpenCloseMap(Width, Height);
        m_OrderedOpenSet = new PriorityQueue<PathNode>(PathNode.Comparer);

        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++)
        {
            if (inGrid[x, y] == null)
                throw new ArgumentNullException();

            m_SearchSpace[x, y] = new PathNode(x, y, inGrid[x, y]);
        }
    }

    public TPathNode[,] SearchSpace { get; }
    public int Width { get; }
    public int Height { get; }

    protected virtual double Heuristic(PathNode inStart, PathNode inEnd)
    {
        return Math.Sqrt((inStart.X - inEnd.X) * (inStart.X - inEnd.X) + (inStart.Y - inEnd.Y) * (inStart.Y - inEnd.Y));
    }

    protected virtual double NeighborDistance(PathNode inStart, PathNode inEnd)
    {
        var diffX = Math.Abs(inStart.X - inEnd.X);
        var diffY = Math.Abs(inStart.Y - inEnd.Y);

        switch (diffX + diffY)
        {
            case 1: return 1;
            case 2: return SQRT_2;
            case 0: return 0;
            default:
                throw new ApplicationException();
        }
    }

    public LinkedList<TPathNode> Search(
        Point inStartNode,
        Point inEndNode,
        TUserContext inUserContext,
        HeightMap hm,
        bool print = true)
    {
        var startNode = m_SearchSpace[inStartNode.X, inStartNode.Y];
        var endNode = m_SearchSpace[inEndNode.X, inEndNode.Y];

        if (startNode == endNode)
            return new LinkedList<TPathNode>(new[] { startNode.UserContext });

        var neighborNodes = new PathNode[8];

        m_ClosedSet.Clear();
        m_OpenSet.Clear();
        m_RuntimeGrid.Clear();
        m_OrderedOpenSet.Clear();

        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++)
            m_CameFrom[x, y] = null;

        startNode.G = 0;
        startNode.H = Heuristic(startNode, endNode);
        startNode.F = startNode.H;

        m_OpenSet.Add(startNode);
        m_OrderedOpenSet.Push(startNode);

        m_RuntimeGrid.Add(startNode);

        var nodes = 0;

        while (!m_OpenSet.IsEmpty)
        {
            var x = m_OrderedOpenSet.Pop();

            if (print)
                hm.PrintBox(x.X, x.Y, hm.Squares[x.X, x.Y].ToString(), ConsoleColor.DarkGray);

            if (x == endNode)
            {
                var result = ReconstructPath(m_CameFrom, m_CameFrom[endNode.X, endNode.Y]);

                result.AddLast(endNode.UserContext);

                return result;
            }

            m_OpenSet.Remove(x);
            if (m_OpenSet.IsEmpty)
            {
            }

            m_ClosedSet.Add(x);

            StoreNeighborNodes(x, neighborNodes);

            for (var i = 0; i < neighborNodes.Length; i++)
            {
                var y = neighborNodes[i];
                bool tentative_is_better;

                if (y == null)
                    continue;

                if (!y.UserContext.IsWalkable(x.UserContext, inUserContext))
                    continue;

                if (m_ClosedSet.Contains(y))
                    continue;

                nodes++;

                var tentative_g_score = m_RuntimeGrid[x].G + NeighborDistance(x, y);
                var wasAdded = false;

                if (!m_OpenSet.Contains(y))
                {
                    m_OpenSet.Add(y);
                    tentative_is_better = true;
                    wasAdded = true;
                }
                else if (tentative_g_score < m_RuntimeGrid[y].G)
                {
                    tentative_is_better = true;
                }
                else
                {
                    tentative_is_better = false;
                }

                if (tentative_is_better)
                {
                    m_CameFrom[y.X, y.Y] = x;

                    if (!m_RuntimeGrid.Contains(y))
                        m_RuntimeGrid.Add(y);

                    m_RuntimeGrid[y].G = tentative_g_score;
                    m_RuntimeGrid[y].H = Heuristic(y, endNode);
                    m_RuntimeGrid[y].F = m_RuntimeGrid[y].G + m_RuntimeGrid[y].H;

                    if (wasAdded)
                        m_OrderedOpenSet.Push(y);
                    else
                        m_OrderedOpenSet.Update(y);
                }
            }

            hm.Squares[x.X, x.Y].CurrentPosition = null;
        }

        return null;
    }

    private LinkedList<TPathNode> ReconstructPath(PathNode[,] came_from, PathNode current_node)
    {
        var result = new LinkedList<TPathNode>();

        ReconstructPathRecursive(came_from, current_node, result);

        return result;
    }

    private void ReconstructPathRecursive(PathNode[,] came_from, PathNode current_node, LinkedList<TPathNode> result)
    {
        var item = came_from[current_node.X, current_node.Y];

        if (item != null)
        {
            ReconstructPathRecursive(came_from, item, result);

            result.AddLast(current_node.UserContext);
        }
        else
        {
            result.AddLast(current_node.UserContext);
        }
    }

    private void StoreNeighborNodes(PathNode inAround, PathNode[] inNeighbors)
    {
        var x = inAround.X;
        var y = inAround.Y;

        if (x > 0 && y > 0)
            inNeighbors[0] = null; //m_SearchSpace[x - 1, y - 1];
        else
            inNeighbors[0] = null;

        if (y > 0)
            inNeighbors[1] = m_SearchSpace[x, y - 1];
        else
            inNeighbors[1] = null;

        if (x < Width - 1 && y > 0)
            inNeighbors[2] = null; //m_SearchSpace[x + 1, y - 1];
        else
            inNeighbors[2] = null;

        if (x > 0)
            inNeighbors[3] = m_SearchSpace[x - 1, y];
        else
            inNeighbors[3] = null;

        if (x < Width - 1)
            inNeighbors[4] = m_SearchSpace[x + 1, y];
        else
            inNeighbors[4] = null;

        if (x > 0 && y < Height - 1)
            inNeighbors[5] = null; //m_SearchSpace[x - 1, y + 1];
        else
            inNeighbors[5] = null;

        if (y < Height - 1)
            inNeighbors[6] = m_SearchSpace[x, y + 1];
        else
            inNeighbors[6] = null;

        if (x < Width - 1 && y < Height - 1)
            inNeighbors[7] = null; //m_SearchSpace[x + 1, y + 1];
        else
            inNeighbors[7] = null;
    }

    protected class PathNode : IPathNode<TUserContext>, IComparer<PathNode>, IIndexedObject
    {
        public static readonly PathNode Comparer = new(0, 0, default);

        public PathNode(int inX, int inY, TPathNode inUserContext)
        {
            X = inX;
            Y = inY;
            UserContext = inUserContext;
        }

        public TPathNode UserContext { get; internal set; }
        public double G { get; internal set; }
        public double H { get; internal set; }
        public double F { get; internal set; }

        public int X { get; internal set; }
        public int Y { get; internal set; }

        public int Compare(PathNode x, PathNode y)
        {
            if (x.F < y.F)
                return -1;
            if (x.F > y.F)
                return 1;

            return 0;
        }

        public int Index { get; set; }

        public bool IsWalkable(IPathNode<TUserContext> fromNode, TUserContext inContext)
        {
            return UserContext.IsWalkable(fromNode, inContext);
        }
    }

    private class OpenCloseMap
    {
        private readonly PathNode[,] m_Map;

        public OpenCloseMap(int inWidth, int inHeight)
        {
            m_Map = new PathNode[inWidth, inHeight];
            Width = inWidth;
            Height = inHeight;
        }

        public int Width { get; }
        public int Height { get; }
        public int Count { get; private set; }

        public PathNode this[int x, int y] => m_Map[x, y];

        public PathNode this[PathNode Node] => m_Map[Node.X, Node.Y];

        public bool IsEmpty => Count == 0;

        public void Add(PathNode inValue)
        {
            var item = m_Map[inValue.X, inValue.Y];

            Count++;
            m_Map[inValue.X, inValue.Y] = inValue;
        }

        public bool Contains(PathNode inValue)
        {
            var item = m_Map[inValue.X, inValue.Y];

            if (item == null)
                return false;

            return true;
        }

        public void Remove(PathNode inValue)
        {
            var item = m_Map[inValue.X, inValue.Y];

            if (!inValue.Equals(item))
                throw new ApplicationException();

            Count--;
            m_Map[inValue.X, inValue.Y] = null;
        }

        public void Clear()
        {
            Count = 0;

            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
                m_Map[x, y] = null;
        }
    }
}