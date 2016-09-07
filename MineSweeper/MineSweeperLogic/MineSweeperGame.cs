using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
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
        public IServiceBus bus;

        public MineSweeperGame(int sizeX, int sizeY, int numberOfMines, IServiceBus bus)
        {
            PosX = posX;
            PosY = posY;
            SizeX = sizeX;
            SizeY = sizeY;
            NumberOfMines = numberOfMines;
            State = state;
            Bus = bus;
        }

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
            Console.WriteLine();
            for (int i = 0; i < SizeY; i++)
            {
                
                for (int j = 0; j < SizeX; j++)
                {
                    

                    if (j == PosX && i == PosY)
                    {
                        Bus.Write("O ", ConsoleColor.Blue);
                    }
                    else
                    {
                        Bus.Write("? ", ConsoleColor.DarkCyan);
                    }
                }
                Console.WriteLine();
                
            }
            Bus.Write("! ");
            //Bus.Write("? ", ConsoleColor.DarkCyan);
            //Bus.Write("X ", ConsoleColor.DarkCyan);
            //Bus.Write("! ", ConsoleColor.DarkCyan);
            //Bus.Write("2 ", ConsoleColor.DarkCyan);
            //Bus.Write(". ", ConsoleColor.DarkCyan);
            //Bus.Write(". ");
            //Bus.Write(". ");
            //Bus.Write(". ");
            //Bus.Write("1 ");
            //Bus.Write("2 ");
            //Bus.Write("3 ");
            //Bus.Write("4 ");
            //Bus.Write("5 ");
            //Bus.Write("6 ");
            //Bus.Write("7 ");
            //Bus.Write("8 ");
            //Bus.Write("? ");
            //Bus.Write("? ");
            //Bus.Write("? ");
            //Bus.Write("! ");
            //Bus.Write("X ");
            //Console.WriteLine();
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
