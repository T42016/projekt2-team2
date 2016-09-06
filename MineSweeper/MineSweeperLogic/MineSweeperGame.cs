using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperLogic
{
    public class MineSweeperGame
    {
        public int posX;
        public int posY;
        public int sizeX;
        public int sizeY;
        public int numberOfMines;
        public GameState state;

        public MineSweeperGame(int sizeX, int sizeY, int nrOfMines, IServiceBus bus)
        {
            PosX = posX;
            PosY = posY;
            SizeX = sizeX;
            SizeY = sizeY;
            NumberOfMines = numberOfMines;
            State = state;
        }

        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        public GameState State { get; private set; }

        public PositionInfo GetCoordinate(int x, int y)
        {
            return null;
        }

        public void FlagCoordinate()
        {

        }

        public void ClickCoordinate()
        {

        }

        public void ResetBoard()
        {

        }

        public void DrawBoard()
        {

        }

        #region MoveCursor Methods

        public void MoveCursorUp()
        {
            PosY -= 1;
        }

        public void MoveCursorDown()
        {
            PosY += 1;
        }

        public void MoveCursorLeft()
        {
            PosX -= 1;
        }

        public void MoveCursorRight()
        {
            PosX += 1;
        }

        #endregion

    }
}
