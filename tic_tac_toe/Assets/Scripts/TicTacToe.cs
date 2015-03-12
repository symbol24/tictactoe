using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TicTacToe : MonoBehaviour {
	public string[][] m_Players = new string[][]{
		new string[]{"Unused", "-"}, new string[]{"Player 1", "X"}, new string[]{"Player 2","O"}
	};
	private int unused = 0;
	private int player1 = 1;
	private int player2 = 2;
	private int m_currentPlayer = 0;
	public int[] m_gameBoard = new int[9];
	private int[] m_gameValues = new int[3]{0,1,2};
	private int[][] m_winningCombinations = new int[][]{
		new int[]{0,1,2},new int[]{3,4,5},new int[]{6,7,8},new int[]{0,3,6},new int[]{1,4,7},new int[]{2,5,8},new int[]{0,4,8},new int[]{2,4,6},
	};
	private Button[] m_buttons = new Button[20];
	private int winner = 0;
	private string m_winningCombo = "";


	// Use this for initialization
	void Start () {
		InitializeBoard();
		m_currentPlayer = GetRandomPlayer ();
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
		if (UpdateGameBoardList (btoChange.name)) {
			Text buttonText = btoChange.GetComponentInChildren<Text> () as Text;
			buttonText.text = m_Players [m_currentPlayer] [1];
			if(!GameOver()) m_currentPlayer = GetNextPlayer (m_currentPlayer);
		}
	}

	private bool GameOver(){
		bool gameOver = false;
		if (CheckGameTied ()) {
			gameOver = true;
			EndGameTied ();
		} else if (CheckIfWon ()) {
			gameOver = true;
			EndGameWon ();
		}
		return gameOver;
	}

	private bool UpdateGameBoardList(string spot){
		int id = int.Parse (spot);
		if (GetBoardSpot(id) == unused) {
			SetBoardSpot(id, m_currentPlayer);
			return true;
		}
		return false;
	}

	private void InitializeBoard(){
		for (int i = 0; i < m_gameBoard.Length; i++) {
			m_gameBoard[i] = m_gameValues[m_currentPlayer];
		}
	}

	private int GetBoardSpot(int id){
		return m_gameBoard [id];
	}

	private void SetBoardSpot(int id, int currentPlaye){
		m_gameBoard [id] = currentPlaye;
	}

	public string GetPlayerName(int playerId){
		return m_Players[playerId][0];
	}

	public string GetPlayerToken(int playerId){
		return m_Players[playerId][1];
	}

	public int GetNextPlayer(int currentPlayer){
		if (currentPlayer == player1)	return player2;
		else return player1;
	}

	private int GetRandomPlayer(){
		return Random.Range(player1,player2);
	}

	private bool TakenBySamePlayer(int a, int b, int c){
		if (m_gameBoard [a] != unused && m_gameBoard [a] == m_gameBoard [b] && m_gameBoard [b] == m_gameBoard [c])
			return true;
		return false;
	}

	private bool CheckIfWon(){
		for (int i = 0; i < m_winningCombinations.Length; i++) {
			if(TakenBySamePlayer(m_winningCombinations[i][0], m_winningCombinations[i][1], m_winningCombinations[i][2])) {
				m_winningCombo = "Winning Combo: " + m_winningCombinations[i][0] + " " +  m_winningCombinations[i][1] + " " + m_winningCombinations[i][2];
				return true;
			}
		}
		return false;
	}

	private bool CheckGameTied(){
		int movesLeft = 9;
		for(int i = 0; i < m_gameBoard.Length; i++){
			if(m_gameBoard[i] != unused)movesLeft--;
		}

		if(movesLeft>0) return false;
		return true;
	}

	private void EndGameTied(){
		print ("Game Tied!");

	}

	private void EndGameWon(){
		print ("Player " + m_currentPlayer + " Won!");
		print (m_winningCombo);
		
	}

	private void PrintBoard(){
		for (int i = 0; i < m_gameBoard.Length; i++)
			print ("Position :" + i + " Player: " + m_gameBoard [i]);

	}
}
