using System;
using static System.Console;

namespace XO
{
    class Program
    {
        string PlayerName1, PlayerName2;
        char[] cells = new char[]{ '-', '-', '-', '-', '-', '-', '-', '-', '-' };

        public Program(string name1, string name2) {
            PlayerName1 = name1;
            PlayerName2 = name2;

            start_game();
        }

        public void show_cells()
        {
            Clear();

            WriteLine("Числа клеток:");
            WriteLine("-1-|-2-|-3-");
            WriteLine("-4-|-5-|-6-");
            WriteLine("-7-|-8-|-9-");
            WriteLine();
            WriteLine("Текущая ситуация (---пустой):");
            WriteLine($"-{cells[0]}-|-{cells[1]}-|-{cells[2]}-");
            WriteLine($"-{cells[3]}-|-{cells[4]}-|-{cells[5]}-");
            WriteLine($"-{cells[6]}-|-{cells[7]}-|-{cells[8]}-");        
        }

        public void do_move(int cell, int num) {
            if (num == 1) cells[cell - 1] = 'X';
            else cells[cell - 1] = 'O';
        }
        public int find_move(int num) {
            string raw_cell;
            int cell = 0;

            if (num == 1) Write(PlayerName1);
            else Write(PlayerName2);

            while (cell > 9 || cell < 1 || cells[cell - 1] == 'O' || cells[cell - 1] == 'X')
            {
                do
                {
                    Write(" введите номер ПУСТОГО поля от 1 до 9:");
                    raw_cell = ReadLine();
                }
                while (!Int32.TryParse(raw_cell, out cell));
                Console.WriteLine();
            }
            return cell;
        }
        public void make_move(int num) { do_move( find_move(num), num ); }

        public char check()
        {
            for (int i = 0; i < 3; i++)
                if (cells[i * 3] == cells[i * 3 + 1] && cells[i * 3 + 1] == cells[i * 3 + 2])
                    return cells[i * 3];
                else if (cells[i] == cells[i + 3] && cells[i + 3] == cells[i + 6])
                    return cells[i];

             if (cells[2] == cells[4] && cells[4] == cells[6]) return cells[2];
             if (cells[0] == cells[4] && cells[4] == cells[8]) return cells[0];
            return '-';
        }

        public void result(char win)
        {
            if (win == 'X')
                WriteLine($"{PlayerName1} вы  выиграли поздравляем.  {PlayerName2} вы проиграли...");
            else if (win == 'O')
                WriteLine($"{PlayerName2} вы  выиграли поздравляем.  {PlayerName1} вы проиграли...");
        }
        public void start_game()
        {
            show_cells();

            char win = '-';
            for (int move = 1; move <= 9; move++)
            {
                if (move % 2 != 0) make_move(1);
                else make_move(2);

                show_cells();

                if (move >= 5) {
                    win = check();
                    if (win != '-')  break;
                }
            }
            result(win);
        }

        static void Main(string[] args)
        {
            string name1, name2;
            do
            {
                Write("Введите имя первого игрока : ");
                name1 = ReadLine();

                Write("Введите имя второго игрока: ");
                name2 = ReadLine();
                WriteLine();
            } while (name1 == name2);

            new Program(name1, name2);
        }
    }
}
