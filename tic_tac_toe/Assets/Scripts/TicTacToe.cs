using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TicTacToe : MonoBehaviour {
	public string[][] m_Players = new string[][]{
		new string[]{"Unused", "-"}, new string[]{"Player 1", "X"}, new string[]{"Player 2","O"}
	};
	private int m_currentPlayer = 0;
	public int[] m_gameBoard = new int[9];
	private int[] m_gameValues = new int[3]{0,1,2};
	private int[][] m_winningCombinations = new int[][]{
		new int[]{0,1,2},new int[]{3,4,5},new int[]{6,7,8},new int[]{0,3,6},new int[]{1,4,7},new int[]{2,5,8},new int[]{0,4,8},new int[]{2,4,6},
	};
	private Button[] m_buttons = new Button[20];


	// Use this for initialization
	void Start () {
		InitializeBoard();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void helpme(){

	}

	public void ButtonPressed(Button thisButton){
		switch (thisButton.name) {
			case "yesButton":
				print("Yes");
				break;
			case "noButton":
				print("no");
				break;
			default:
				ChangeButton(thisButton);
				break;
		}
	}

	private void ChangeButton(Button btoChange){
		Text buttonText = btoChange.GetComponentInChildren<Text>() as Text;
		buttonText.text = m_Players [m_currentPlayer][1];
		for (int i = 0; i < m_gameBoard.Length; i++) {
			if (i.ToString () == buttonText.text)
				m_gameBoard[i] = m_currentPlayer;
		}
	}

	private void InitializeBoard(){
		for (int i = 0; i < m_gameBoard.Length; i++) {
			m_gameBoard[i] = m_gameValues[m_currentPlayer];
		}
		m_currentPlayer = 1;
	}
}
