using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TicTacToe : MonoBehaviour {
	public string[][] m_Players = new string[][]{
		new string[]{"Unused", "-"}, new string[]{"Player 1", "X"}, new string[]{"Player 2","O"}
	};
	private int m_unused = 0;
	private int m_player1 = 1;
	private int m_player2 = 2;
	private int m_currentPlayer = 0;
	public int[] m_gameBoard = new int[9];
	private int[] m_gameValues = new int[3]{0,1,2};
	private int[][] m_winningCombinations = new int[][]{
		new int[]{0,1,2},new int[]{3,4,5},new int[]{6,7,8},new int[]{0,3,6},new int[]{1,4,7},new int[]{2,5,8},new int[]{0,4,8},new int[]{2,4,6},
	};
	private Button[] m_buttons = new Button[20];
	private string m_winningCombo = "";
	private int m_winningMove = -1;


	// Use this for initialization
	void Start () {
		InitializeBoard();
		m_currentPlayer = GetRandomPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ButtonPressed(Button thisButton){
		switch (thisButton.name) {
			case "ResetButton":
				ResetGame();
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
			if(!GameOver(m_gameBoard)) {
				m_currentPlayer = GetNextPlayer (m_currentPlayer);
				if(m_currentPlayer == m_player2){
					int computerMove = ComputeMove(m_gameBoard, m_currentPlayer, -1);
					print (computerMove.ToString());
					Button nextButton = GetButton(computerMove.ToString());
					ChangeButton(nextButton);
				}
			}
		}
	}

	private bool GameOver(int[] board){
		bool gameOver = false;
		if (CheckGameTied (board)) {
			gameOver = true;
			EndGameTied ();
		} else if (CheckIfWon (board)) {
			gameOver = true;
			EndGameWon ();
		}
		return gameOver;
	}

	private bool UpdateGameBoardList(string spot){
		int id = int.Parse (spot);
		if (GetBoardSpot(id) == m_unused) {
			SetBoardSpot(id, m_currentPlayer, m_gameBoard);
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

	private void SetBoardSpot(int id, int currentPlayer, int[] board){
		board [id] = currentPlayer;
	}

	public string GetPlayerName(int playerId){
		return m_Players[playerId][0];
	}

	public string GetPlayerToken(int playerId){
		return m_Players[playerId][1];
	}

	public int GetNextPlayer(int currentPlayer){
		if (currentPlayer == m_player1)	return m_player2;
		else return m_player1;
	}

	public void SetPlayerUnset(){
		m_currentPlayer = m_unused;
	}

	private int GetRandomPlayer(){
		return Random.Range(m_player1,m_player2);
	}

	private bool TakenBySamePlayer(int[] board, int a, int b, int c){
		if (m_gameBoard [a] != m_unused && m_gameBoard [a] == m_gameBoard [b] && m_gameBoard [b] == m_gameBoard [c])
			return true;
		return false;
	}

	private bool CheckIfWon(int[] board){
		for (int i = 0; i < m_winningCombinations.Length; i++) {
			if(TakenBySamePlayer(board, m_winningCombinations[i][0], m_winningCombinations[i][1], m_winningCombinations[i][2])) {
				m_winningCombo = "Winning Combo: " + m_winningCombinations[i][0] + " " +  m_winningCombinations[i][1] + " " + m_winningCombinations[i][2];
				return true;
			}
		}
		return false;
	}

	private bool CheckGameTied(int[] board){
		int movesLeft = board.Length;
		for(int i = 0; i < board.Length; i++){
			if(board[i] != m_unused)movesLeft--;
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

	public void ResetGame(){
		SetPlayerUnset ();
		InitializeBoard ();
		ResetButtons ();
		m_currentPlayer = GetRandomPlayer ();
	}

	private void ResetButtons(){
		for(int i = 0; i < m_gameBoard.Length; i++){
			Button bto = GetButton(i.ToString());
			Text buttonText = bto.GetComponentInChildren<Text> () as Text;
			buttonText.text = m_Players [m_currentPlayer] [1];
		}
	}

	private Button GetButton(string buttonName){
		return GameObject.Find(buttonName).GetComponent<Button>() as Button;
	}

	private int ComputeMove(int[] board, int currentPlayer, int previousMove){
		int newMovePosition = -1;

		if(CheckIfWon(board)) return previousMove;

		int newWinner = -1;

		for(int i = 0; i < board.Length; i++){
			if(board[i] == m_unused){
				SetBoardSpot(i, currentPlayer, board);
				currentPlayer = GetNextPlayer(currentPlayer);
				newMovePosition = ComputeMove(board, currentPlayer, m_unused);
				SetBoardSpot(i, m_unused, board);

				//print ("CurrenPlayer: " + currentPlayer + "  previousWinner: " + previousWinner + " newMovePosition: " + newMovePosition);

				if((currentPlayer == m_player2 && previousMove == m_player1) || (currentPlayer == m_player1 && previousMove == m_player2)){
					newWinner = currentPlayer;
					newMovePosition = i;
				}

				if(newWinner == m_player2){
					return newMovePosition;
				}
			}
		}

		if(newWinner == -1){
			return newWinner;
		}

		return newMovePosition;
	}


}
