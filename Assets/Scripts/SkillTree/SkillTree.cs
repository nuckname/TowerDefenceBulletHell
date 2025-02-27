using UnityEngine;
using System.Collections.Generic;

public class SkillTreeDebugger : MonoBehaviour
{
    [System.Serializable]
    public class SkillNode
    {
        public Vector3 position;
        public GameObject nodeObject;
        public List<SkillNode> children = new List<SkillNode>();
        public bool isExtraLarge = false; // Flag to indicate extra-large node

        public SkillNode(Vector3 pos, Transform parent)
        {
            position = pos;
            nodeObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            nodeObject.transform.position = pos;
            nodeObject.transform.localScale = Vector3.one * 0.5f;
            nodeObject.transform.SetParent(parent);
        }
    }

    public int maxDepth = 5;
    public int maxChildren = 3;
    public float minSpread = 1.5f;
    public float maxSpread = 3.5f;
    public float nodeRadius = 1.0f; // Prevents overlap
    public Material lineMaterial;

    // Extra Large Node Parameters
    public float extraLargeChance = 0.3f;         // Chance to add an extra large branch at a node (if not forcing)
    public float extraLargeSpreadMultiplier = 1.5f; // Multiplier for spread distance for extra large nodes
    public float extraLargeNodeScale = 1.5f;        // Scale for extra large nodes

    // New variable: if true, every node will automatically attach a big node
    public bool alwaysAttachBigNode = false;

    private Transform treeRoot;
    private List<GameObject> lines = new List<GameObject>();
    private List<Vector3> existingNodes = new List<Vector3>();
    private List<(Vector3, Vector3)> existingLines = new List<(Vector3, Vector3)>();

    private SkillNode rootNode; // store root of tree

    void Start()
    {
        GenerateTree();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTree();
        }
    }

    void GenerateTree()
    {
        // Clean up existing tree and lines
        if (treeRoot != null) Destroy(treeRoot.gameObject);
        foreach (GameObject line in lines) Destroy(line);
        lines.Clear();
        existingNodes.Clear();
        existingLines.Clear();

        treeRoot = new GameObject("SkillTreeRoot").transform;
        Vector3 rootPosition = Vector3.zero;
        SkillNode root = new SkillNode(rootPosition, treeRoot);
        existingNodes.Add(rootPosition);
        rootNode = root;

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        List<SkillNode> mainBranches = new List<SkillNode>();

        foreach (Vector3 dir in directions)
        {
            Vector3 childPosition = GetValidPosition(rootPosition, dir);
            if (childPosition != Vector3.zero)
            {
                SkillNode child = new SkillNode(childPosition, treeRoot);
                root.children.Add(child);
                mainBranches.Add(child);
                DrawLine(root.position, child.position);
            }
        }

        foreach (SkillNode branch in mainBranches)
        {
            GenerateBranch(branch, Random.Range(2, maxDepth));
        }

        // After generating the main tree, attach extra-large (big) nodes where appropriate.
        GenerateExtraLargeBranches(rootNode);
    }

    void GenerateBranch(SkillNode node, int remainingDepth)
    {
        if (remainingDepth <= 0) return;

        int numChildren = Random.Range(1, maxChildren + 1);
        for (int i = 0; i < numChildren; i++)
        {
            Vector3 direction = Random.insideUnitCircle.normalized;
            Vector3 childPosition = GetValidPosition(node.position, direction);

            if (childPosition != Vector3.zero)
            {
                SkillNode child = new SkillNode(childPosition, treeRoot);
                node.children.Add(child);
                DrawLine(node.position, child.position);

                GenerateBranch(child, remainingDepth - Random.Range(1, 3));
            }
        }
    }

    // Public method called when an extra-large node is clicked.
    public void ExpandLargeNode(SkillNode node)
    {
        // Expand further from the clicked node with a random extra depth.
        int extraDepth = Random.Range(2, maxDepth);
        GenerateBranch(node, extraDepth);
    }

    Vector3 GetValidPosition(Vector3 parentPosition, Vector3 direction)
    {
        for (int attempt = 0; attempt < 10; attempt++) // Try 10 times to find a valid position
        {
            float spread = Random.Range(minSpread, maxSpread);
            Vector3 potentialPosition = parentPosition + direction * spread;

            // Ensure the new node does not overlap an existing node
            bool tooClose = false;
            foreach (Vector3 existing in existingNodes)
            {
                if (Vector3.Distance(existing, potentialPosition) < nodeRadius * 2)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose && !IntersectsExistingLines(parentPosition, potentialPosition))
            {
                existingNodes.Add(potentialPosition);
                existingLines.Add((parentPosition, potentialPosition)); // Store new connection
                return potentialPosition;
            }
        }
        return Vector3.zero; // Failed to find a valid position
    }

    // New method to get a valid position for an extra-large node
    Vector3 GetValidPositionForExtraLarge(Vector3 parentPosition, Vector3 direction)
    {
        for (int attempt = 0; attempt < 10; attempt++)
        {
            float spread = Random.Range(minSpread * extraLargeSpreadMultiplier, maxSpread * extraLargeSpreadMultiplier);
            Vector3 potentialPosition = parentPosition + direction * spread;

            bool tooClose = false;
            foreach (Vector3 existing in existingNodes)
            {
                if (Vector3.Distance(existing, potentialPosition) < (nodeRadius * extraLargeNodeScale) * 2)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose && !IntersectsExistingLines(parentPosition, potentialPosition))
            {
                existingNodes.Add(potentialPosition);
                existingLines.Add((parentPosition, potentialPosition));
                return potentialPosition;
            }
        }
        return Vector3.zero;
    }

    // Helper method to check if a node already has an extra-large child.
    bool HasExtraLargeChild(SkillNode node)
    {
        foreach (SkillNode child in node.children)
        {
            if (child.isExtraLarge)
                return true;
        }
        return false;
    }

    // Recursively traverse the tree and add extra-large (big) branches.
    // If alwaysAttachBigNode is true, then each node gets a big node attached (if it doesn’t already).
    void GenerateExtraLargeBranches(SkillNode node)
    {
        bool attachBig = alwaysAttachBigNode || (!node.isExtraLarge && Random.value < extraLargeChance);
        if (attachBig && !HasExtraLargeChild(node))
        {
            Vector3 direction = Random.insideUnitCircle.normalized;
            Vector3 extraPosition = GetValidPositionForExtraLarge(node.position, direction);
            if (extraPosition != Vector3.zero)
            {
                SkillNode extraNode = new SkillNode(extraPosition, treeRoot);
                // Mark as extra large and set its appearance to yellow.
                extraNode.nodeObject.transform.localScale = Vector3.one * extraLargeNodeScale;
                extraNode.nodeObject.GetComponent<Renderer>().material.color = Color.yellow;
                extraNode.isExtraLarge = true;
                node.children.Add(extraNode);
                DrawLine(node.position, extraNode.position);

                // Attach click handler for further expansion.
                NodeClickHandler clickHandler = extraNode.nodeObject.AddComponent<NodeClickHandler>();
                clickHandler.skillNode = extraNode;
                clickHandler.skillTreeDebugger = this;
            }
        }

        // Recursively attempt extra branch generation for each child.
        foreach (SkillNode child in node.children)
        {
            GenerateExtraLargeBranches(child);
        }
    }

    bool IntersectsExistingLines(Vector3 start, Vector3 end)
    {
        foreach (var line in existingLines)
        {
            if (LinesIntersect(start, end, line.Item1, line.Item2))
                return true;
        }
        return false;
    }

    bool LinesIntersect(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        // 2D line intersection using x and y coordinates.
        Vector2 AB = new Vector2(B.x - A.x, B.y - A.y);
        Vector2 CD = new Vector2(D.x - C.x, D.y - C.y);
        Vector2 AC = new Vector2(C.x - A.x, C.y - A.y);
        Vector2 AD = new Vector2(D.x - A.x, D.y - A.y);

        float cross1 = AB.x * AC.y - AB.y * AC.x;
        float cross2 = AB.x * AD.y - AB.y * AD.x;
        float cross3 = CD.x * AC.y - CD.y * AC.x;
        float cross4 = CD.x * AD.y - CD.y * AD.x;

        return (cross1 * cross2 < 0) && (cross3 * cross4 < 0);
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject lineObject = new GameObject("Line");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.material = lineMaterial ?? new Material(Shader.Find("Sprites/Default"));

        lines.Add(lineObject);
    }
}

// This component is attached to extra-large nodes to handle mouse clicks.
public class NodeClickHandler : MonoBehaviour
{
    public SkillTreeDebugger.SkillNode skillNode;
    public SkillTreeDebugger skillTreeDebugger;

    void OnMouseDown()
    {
        // When an extra-large (yellow) node is clicked, expand it further.
        if (skillNode != null && skillTreeDebugger != null && skillNode.isExtraLarge)
        {
            skillTreeDebugger.ExpandLargeNode(skillNode);
        }
    }
}
