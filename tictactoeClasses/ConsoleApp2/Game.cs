
namespace GameState;
public class Game
{
    private Board board;
    private PLAYER currentPlayer;
    private int moves;

    public Game()
    {
        board = new Board();
        currentPlayer = PLAYER.PLAYER1;
        moves = 0;
    }

    public void Start()
    {
        bool gameWon = false;

        while (moves < 9 && !gameWon)
        {
            if (moves != 0)
            {
                currentPlayer = (currentPlayer == PLAYER.PLAYER1) ? PLAYER.PLAYER2 : PLAYER.PLAYER1;
            }
            Console.Clear();
            board.Print();
            Console.WriteLine($"Player {currentPlayer}, make your move:");

            int choice = InputHandler.GetValidInput(board);
            board.Update(choice, currentPlayer);
            gameWon = board.CheckWinner();

            moves++;
        }

        Console.Clear();
        board.Print();
        Console.WriteLine(gameWon ? $"{currentPlayer} has won!" : "It's a tie!");
    }
}
