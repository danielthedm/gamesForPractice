using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using GameState;
namespace GameState.Tests;

[TestClass]
public class GameTests
{
    [TestMethod]
    public void Game_InitializesCorrectly()
    {
        // Arrange
        Game game = new Game();

        // Act (reflection to check private fields)
        var boardField = typeof(Game).GetField("board", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var playerField = typeof(Game).GetField("currentPlayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var movesField = typeof(Game).GetField("moves", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var board = boardField.GetValue(game);
        var currentPlayer = (PLAYER)playerField.GetValue(game);
        var moves = (int)movesField.GetValue(game);

        // Assert
        Assert.IsNotNull(board);
        Assert.AreEqual(PLAYER.PLAYER1, currentPlayer);
        Assert.AreEqual(0, moves);
    }

    [TestMethod]
    public void Player_Switches_After_Move()
    {
        // Arrange
        Game game = new Game();

        // Access private field using reflection
        var playerField = typeof(Game).GetField("currentPlayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var movesField = typeof(Game).GetField("moves", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        playerField.SetValue(game, PLAYER.PLAYER1);
        movesField.SetValue(game, 1);

        game.Start(); // Run one iteration manually

        // Assert
        var newPlayer = (PLAYER)playerField.GetValue(game);
        Assert.AreEqual(PLAYER.PLAYER2, newPlayer);
    }

    [TestMethod]
    public void Game_Ends_With_Winner()
    {
        // Arrange
        Game game = new Game();
        Board board = new Board();
        var boardField = typeof(Game).GetField("board", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        boardField.SetValue(game, board);

        // Simulate a winning condition
        board.Update(1, PLAYER.PLAYER1);
        board.Update(2, PLAYER.PLAYER1);
        board.Update(3, PLAYER.PLAYER1);

        var checkWinnerMethod = typeof(Board).GetMethod("CheckWinner", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        bool gameWon = (bool)checkWinnerMethod.Invoke(board, null);

        // Assert
        Assert.IsTrue(gameWon);
    }

    [TestMethod]
    public void Game_Ends_With_Draw()
    {
        // Arrange
        Game game = new Game();
        Board board = new Board();
        var boardField = typeof(Game).GetField("board", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        boardField.SetValue(game, board);

        // Simulate a draw condition
        board.Update(1, PLAYER.PLAYER1);
        board.Update(2, PLAYER.PLAYER2);
        board.Update(3, PLAYER.PLAYER1);
        board.Update(4, PLAYER.PLAYER2);
        board.Update(5, PLAYER.PLAYER1);
        board.Update(6, PLAYER.PLAYER2);
        board.Update(7, PLAYER.PLAYER1);
        board.Update(8, PLAYER.PLAYER2);
        board.Update(9, PLAYER.PLAYER1);

        var checkWinnerMethod = typeof(Board).GetMethod("CheckWinner", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        bool gameWon = (bool)checkWinnerMethod.Invoke(board, null);

        // Assert
        Assert.IsFalse(gameWon);
    }
}
