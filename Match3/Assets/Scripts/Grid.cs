using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class Grid : MonoBehaviour {
    public GridNode gridNodePrefab;
	public GridObj objPrefab;
    private GridNode[ , ] _nodes;
    private float _node_width;
    private float _node_height;
	private float gridsize=1.1f;

public Color color1, color2;
		public Vector2 size { get { return new Vector2 (_node_width * _nodes.GetLength (1), _node_height * _nodes.GetLength (0)); } }


	GridObj CreateObject (GridNode node)
	{
		GridObj obj = Instantiate<GridObj> (objPrefab);
		obj.name = string.Format ("Obj {0}{1}", (char)('A' + node.row), node.column);

		Vector2 pos = new Vector2 (node.row, node.column);
		foreach (Vector2 redObj in GameManager.instance.yellowGrid){
			if((redObj - pos).magnitude < 0.1f){
				obj.colortype = GridObj.ColorType.yellow;
			}
		}
		foreach (Vector2 redObj in GameManager.instance.redGrid) {
			if ((redObj - pos).magnitude < 0.1f) {
				obj.colortype = GridObj.ColorType.red;
			}
		};
		//obj.transform.position = node.transform.position
		obj.transform.SetParent (node.transform);
		obj.gameObject.SetActive (true);
		return obj;
	}

	private GridNode CreateNode( int row, int col ) {
        GridNode node = Instantiate<GridNode>( gridNodePrefab );
        node.name = string.Format( "Node {0}{1}", (char)('A'+row), col );
        node.grid = this;
        node.row = row;
        node.column = col;
		node.transform.SetParent( transform);
        node.gameObject.SetActive( true );
		node.transform.localScale = new Vector3 (gridsize,gridsize,gridsize);
		if ((row + col) % 2 == 0)
			node.SetColor (color1);
		else
			node.SetColor (color2);

		node.SetObject (CreateObject (node));
        return node;
    }

    public void Create(int rows, int columns) {

        _node_width = gridNodePrefab.GetComponent<Renderer>().bounds.size.x* gridsize;
        _node_height = gridNodePrefab.GetComponent<Renderer>().bounds.size.y* gridsize;
        Vector2 node_position = new Vector2( _node_width * 0.5f, _node_height * -0.5f );
        _nodes = new GridNode[ rows, columns ];
        for( int row = 0; row < rows; ++row ) {
            for( int col = 0; col < columns; ++col ) {
                GridNode node = CreateNode( row, col );
                node.transform.localPosition = node_position;
                _nodes[ row, col ] = node;

                node_position.x += _node_width;


				//node.SetObject ();
            }
            node_position.x = _node_width * 0.5f;
            node_position.y -= _node_height;
        }
    }

	private void DeleteNode(int row, int col){
		GameObject delete = GameObject.Find (string.Format( "Node {0}{1}", (char)('A'+row-1), col-1 ));
		delete.gameObject.SetActive(false);
		
	}

	public List<GridNode> AllNode{ get { 
			List<GridNode> allnode =new List<GridNode>();
			for (int row = 0; row < _nodes.GetLength(0); ++row) {
				for (int col = 0; col < _nodes.GetLength(1); ++col) {

					allnode.Add (_nodes [row, col]);
				}
			}
			return allnode;
		} }

    public GridNode GetNode( int row, int col ) {
		if(row>=0&&col >=0 && row<_nodes.GetLength(0)&& col < _nodes.GetLength(1))
			return _nodes[row, col];
		return null;
    }


	public IList<GridNode> GetNodeNeighbors( int row, int col, GridObj.ColorType color ) {
        IList<GridNode> neighbors = new List<GridNode>();

        int start_row = Mathf.Max( row - 1, 0 );
        int start_col = Mathf.Max( col - 1, 0 );
        int end_row = Mathf.Min( row + 1, _nodes.GetLength( 0 ) - 1 );
        int end_col = Mathf.Min( col + 1, _nodes.GetLength( 1 ) - 1 );

        for( int row_index = start_row; row_index <= end_row; ++row_index ) {
            for( int col_index = start_col; col_index <= end_col; ++col_index ) {
				if(row_index == row || col_index == col)
           		{
					if(_nodes [row_index, col_index].ObjColorType == color)
					neighbors.Add (_nodes [row_index, col_index]); }

            }
        }

		neighbors.Remove (_nodes [row, col]);
        return neighbors;
    }

	
	private Vector2 MouseRC{ get { 
			Vector3 world_pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3 local_pos = transform.InverseTransformPoint (world_pos);
			// This trick makes a lot of assumptions that the nodes haven't been modified since initialization.
			int column = Mathf.FloorToInt (local_pos.x / _node_width);
			int row = Mathf.FloorToInt (-local_pos.y / _node_height);
			return new Vector2 (row, column);
		
		} }

	public void SwapObj(GridNode node1, GridNode node2){
		//node1.GridObj.transform.SetParent (node2.transform,false);
		//node2.GridObj.transform.SetParent (node1.transform,false)
		GridObj.ColorType col = node1.ObjColorType;;
		node1.SetObjColor (node2.ObjColorType);
		node2.SetObjColor (col);
	}

	public GridNode MouseNode{get{
			int row = Mathf.RoundToInt( MouseRC.x);
			int col = Mathf.RoundToInt(MouseRC.y);
			if (MouseRC.x >= 0 && MouseRC.x < _nodes.GetLength (0)
				&& MouseRC.y >= 0 && MouseRC.y < _nodes.GetLength (1)) {
				GridNode node = _nodes [row,col];
				return node;
			} else {
				return null;
			}
		}
	}


	void Update(){
		//if (Input.GetKeyDown (KeyCode.Space))
		//	StartCoroutine( SwapRow (5));
		//_nodes [5, 0].ChangeObjColor ();

		//SwapObj (GetNode (5, 1), GetNode (5,2));
	}
}
