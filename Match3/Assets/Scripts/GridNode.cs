using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
//[ExecuteInEditMode]

public class GridNode : MonoBehaviour {
    public Grid grid;
    public int column;
    public int row;
    public SpriteRenderer _renderer;
	GridObj gridObj;

	public bool scoreChecked = false;
	public GridObj GridObj{ get { return gridObj;}}

	public void SetColor(Color color){
		 GetComponent<SpriteRenderer> ().color = color;
	}

	public GridObj.ColorType ObjColorType{ get { return gridObj.colortype;}}

	public void SetObject(GridObj obj){
		//obj.transform.position = transform.position;
		gridObj = obj;
	}

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }


	public IList<GridNode> GetNeighbors(GridObj.ColorType col) {
		return grid.GetNodeNeighbors( row, column, col);
    }

	public void SetObjColor(GridObj.ColorType col){
		gridObj.colortype = col;
		gridObj.ColorCheck ();
	}

	public void ChangeObjColor(){
		if (gridObj.colortype == GridObj.ColorType.blue)
			gridObj.colortype = GridObj.ColorType.yellow;
		else
			gridObj.colortype = GridObj.ColorType.blue;

		gridObj.ColorCheck ();
	}



	void Start(){
		//gridObj
	}
	
	private void Update(){
	}
}
						
