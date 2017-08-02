using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class GameManager : MonoBehaviour {
	public Grid grid;
	int GRIDROWS= 7;
	int GRIDCOLS=12;
	public Vector2 [] redGrid;
	public Vector2[] yellowGrid;
	public static GameManager instance;
	bool gameWin = false;
	int clicks = 0;
	public Text levelText;
	public Text clicksText;
	GridObj.ColorType curColorType = GridObj.ColorType.yellow;
	Button [] buttons;
	
	// Use this for initialization
	void Awake(){
		instance = this;
		
	}

	bool CheckWholeGrid(){
		foreach (GridNode node in grid.AllNode){
			if (node.ObjColorType != GridObj.ColorType.blue)
				return false;
		}
		return true;
	}

	bool CheckMatch(GridNode node, GridObj.ColorType col){
		if (node.ObjColorType != col)
			return false;

		if (node.GetNeighbors (col).Count != 0) {
			//Debug.Log ();
			List<GridNode> MatchedList = new List<GridNode> ();
			foreach (GridNode neighbor in node.GetNeighbors (col)) {
				int rowDiff = neighbor.row - node.row;
				int colDiff = neighbor.column - node.column;
				GridNode tarNode1 = grid.GetNode (node.row + 2 * rowDiff, node.column + 2 * colDiff);
				GridNode tarNode2 = grid.GetNode (node.row - rowDiff, node.column - colDiff);
				GridNode tarNode3 = grid.GetNode (node.row - 2 * rowDiff, node.column - 2 * colDiff);

				if (tarNode1 != null && tarNode1.ObjColorType == col) {
					MatchedList.Add (node);
					MatchedList.Add (neighbor);
					MatchedList.Add (tarNode1);
					if (tarNode2 != null && tarNode2.ObjColorType == col) {
						MatchedList.Add (tarNode2);
						if (tarNode2 != null && tarNode3.ObjColorType == col)
							MatchedList.Add (tarNode3);
					}
				} else if (tarNode2 != null && tarNode2.ObjColorType == col) {
					MatchedList.Add (node);
					MatchedList.Add (neighbor);
					MatchedList.Add (tarNode2);

				}
			}
			if(MatchedList.Count!=0){
				foreach (GridNode nodes in MatchedList) {
					StartCoroutine (nodes.GridObj.Matched (col));
					//nodes.SetObjColor (GridObj.ColorType.blue);
					gameWin = CheckWholeGrid ();
					//StopCoroutine (SwapRow (node.row));
				}
				return true;
			}
		} 
			return false;
	}

	public IEnumerator SwapRow (int rowNum, GridObj.ColorType ctype)
	{
		grid.GetNode(rowNum, 0).SetObjColor (ctype);
		CheckMatch (grid.GetNode (rowNum, 0), ctype);
		
		yield return new WaitForSeconds (.5f);

		for (int i = 0; i < GRIDCOLS - 1; i++) {
			grid.SwapObj (grid.GetNode(rowNum, i), grid.GetNode (rowNum, i + 1));
			
			if(!CheckMatch (grid.GetNode(rowNum, i+1),ctype))
			yield return new WaitForSeconds (.5f);
			else
				yield break;
		}
		//_nodes [rowNum, column - 1].ChangeObjColor();
	}

	public IEnumerator SwapCol (int colNum, GridObj.ColorType ctype)
	{
		grid.GetNode (GRIDROWS-1, colNum).SetObjColor (ctype);
		CheckMatch (grid.GetNode (GRIDROWS - 1, colNum),ctype);

		yield return new WaitForSeconds (.5f);

		for (int i = GRIDROWS - 1; i >0; i--) {
			grid.SwapObj (grid.GetNode (i, colNum), grid.GetNode (i-1, colNum));
			
			if(!CheckMatch (grid.GetNode (i-1, colNum),ctype))
			yield return new WaitForSeconds (.5f);
			else
				yield break;
		}
		//_nodes [rowNum, column - 1].ChangeObjColor();
	}

	public void StartSwapCol(int col){
		clicks++;
		GridObj.ColorType color = curColorType;
		
		StartCoroutine (SwapCol (col,color));
	}

	public void StartSwapRow (int row)
	{
		clicks++;
		GridObj.ColorType col = curColorType;
		StartCoroutine (SwapRow (row,col));
	}

	void Start () {
		grid.Create (GRIDROWS, GRIDCOLS);
		buttons = FindObjectsOfType<Button> ();
		levelText.text = "Level " + (SceneManager.GetActiveScene ().buildIndex +1);
		//grid.transform.position = Camera.main.ViewportToWorldPoint (new Vector2 (-1, 0));
	}

	void ChangeButtonColor(Color col){
		foreach (Button but in buttons)
			but.image.color = col;
	}

	void SwitchCurColorType(){

		if (curColorType == GridObj.ColorType.red) {
			curColorType = GridObj.ColorType.yellow;
			ChangeButtonColor (Color.yellow);
		} else {
			curColorType = GridObj.ColorType.red;
			ChangeButtonColor (Color.red);
		}
	}

	void NextLevel(){
		int nextLevelIndex = SceneManager.GetActiveScene ().buildIndex < SceneManager.sceneCountInBuildSettings - 1 ? SceneManager.GetActiveScene ().buildIndex+1:0;
		SceneManager.LoadScene (nextLevelIndex);
	}

	void PrevLevel(){
		int prevLevelIndex = SceneManager.GetActiveScene ().buildIndex > 0 ? SceneManager.GetActiveScene ().buildIndex - 1 : 0;
		SceneManager.LoadScene (prevLevelIndex);
	}

	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown (KeyCode.Space))
		//	StartCoroutine( SwapCol (5));

		clicksText.text = "Clicks: " + clicks;
		if (Input.GetKeyDown (KeyCode.R))
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);


		if(Input.GetKeyDown(KeyCode.Tab)){
			SwitchCurColorType ();
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			PrevLevel ();
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			NextLevel ();
		}

		if (gameWin)
			NextLevel ();

	}
		

}
