using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperLogic
{
    public class MineSweeperGame
    {
        private int posX;
        private int posY;
        private int sizeX;
        private int sizeY;
        private int numberOfMines;
        private GameState state;
        private IServiceBus bus;
        
        public MineSweeperGame(int sizeX, int sizeY, int numberOfMines, IServiceBus bus)
        {
            PosX = posX;
            PosY = posY;
            SizeX = sizeX;
            SizeY = sizeY;
            NumberOfMines = numberOfMines;
            State = state;
            Bus = bus;
            positions = new PositionInfo[SizeX, SizeY];

            ResetBoard();
        }

        private PositionInfo[,] positions;
        public ConsoleColor DarkCyan { get; set; }
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        public GameState State { get; private set; }
        public IServiceBus Bus { get; }

        public PositionInfo GetCoordinate(int x, int y)
        {
            return positions[x,y];

        }

        public void FlagCoordinate()
        {

        }

        public void ClickCoordinate()
        {
            
        }

        public void ResetBoard()
        {
            State = GameState.Playing;

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    positions[x, y] = new PositionInfo();
                    positions[x, y].IsOpen = false;
                    positions[x, y].HasMine = false;
                    positions[x, y].IsFlagged = false;
                    positions[x, y].NrOfNeighbours = 0;
                    positions[x, y].X = x;
                    positions[x, y].Y = y;
                }
            }

            for (int i = 0; i < NumberOfMines; i++)
            {
                Random Ran = new Random();

                int ranX = Ran.Next(0, SizeX);
                int ranY = Ran.Next(0, SizeY);

                if (positions[ranX, ranY].HasMine == false)
                {
                    positions[ranX, ranY].HasMine = true;
                }
                else
                {
                    i--;
                }
            }
        }

        public void DrawBoard()
        {
            for (int i = 0; i < SizeY; i++)
            {
                
                for (int j = 0; j < SizeX; j++)
                {
                    

                    if (j == PosX && i == PosY)
                    {
                        Bus.Write("? ", ConsoleColor.DarkCyan);
                    }
                    else
                    {
                        Bus.Write("? ");
                    }
                }
                Console.WriteLine();
                
            }

        }

        #region MoveCursor Methods

        public void MoveCursorUp()
        {
            if (PosY > 0)
            {
                PosY -= 1;
            }
        }

        public void MoveCursorDown()
        {
            if (PosY < SizeY - 1)
            {
                PosY += 1;
            }
        }

        public void MoveCursorLeft()
        {
            if (PosX > 0)
            {
                PosX -= 1;
            }
        }

        public void MoveCursorRight()
        {
            if (PosX < SizeX - 1)
            {
                PosX += 1;
            }
        }

        #endregion

    }
}
