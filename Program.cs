using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjektTreIRad
{
    internal class Program
    {
        // Här skapar vi en spelplan, en 3x3 matris där varje ruta är numrerad från 1 till 9.
        static char[,] board = {
        { '1', '2', '3' },
        { '4', '5', '6' },
        { '7', '8', '9' }
    };

        // Denna variabel håller reda på vem som spelar just nu – antingen 'X' eller 'O'.
        static char currentPlayer = 'X';

        static void Main()
        {
            bool gameRunning = true;  // Spelet är igång så länge denna variabel är true.

            // Här börjar spelet. Loopen fortsätter tills någon vinner eller vi får oavgjort.
            while (gameRunning)
            {
                Console.Clear();  // Rensar skärmen för att göra plats för nästa drag.
                DrawBoard();  // Vi ritar ut spelplanen så att spelarna kan se sitt nästa drag.
                PlayerMove();  // Låter den aktuella spelaren göra sitt drag.

                // Kontrollera om någon har vunnit.
                if (CheckWinner())
                {
                    Console.Clear();  // Rensa skärmen för att visa slutresultatet.
                    DrawBoard();  // Skriver ut spelplanen så att vi kan se resultatet.
                    Console.WriteLine($"\nGrattis! Spelare {currentPlayer} vinner!");  // Meddelar vinnaren.
                    gameRunning = false;  // Spelet är slut.
                }
                // Om spelplanen är full, och ingen har vunnit, betyder det oavgjort.
                else if (IsFull())
                {
                    Console.Clear();  // Rensa skärmen för att visa resultatet.
                    DrawBoard();  // Skriver ut den slutgiltiga spelplanen.
                    Console.WriteLine("\nOavgjort! Inga fler drag kvar.");  // Meddelar att ingen vann.
                    gameRunning = false;  // Spelet är slut.
                }
                else
                {
                    SwitchPlayer();  // Växlar till nästa spelare om ingen har vunnit än.
                }
            }
        }

        // Denna metod skriver ut hela spelplanen. Spelarna behöver se var de kan göra sina drag.
        static void DrawBoard()
        {
            Console.WriteLine("Välkommen till Tre i Rad!");

            // Här skriver vi ut spelets spelplan rad för rad.
            Console.WriteLine($" {board[0, 0]} | {board[0, 1]} | {board[0, 2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[1, 0]} | {board[1, 1]} | {board[1, 2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[2, 0]} | {board[2, 1]} | {board[2, 2]} ");
        }

        // Här låter vi spelaren göra sitt drag. De väljer en siffra mellan 1 och 9 för att placera sin symbol.
        static void PlayerMove()
        {
            bool validMove = false;  // Vi kommer att hålla koll på om draget är giltigt.

            // Vi låter spelaren göra om sitt drag tills de gör ett giltigt drag.
            while (!validMove)
            {
                Console.Write($"\nSpelare {currentPlayer}, välj en ruta (1-9): ");
                string input = Console.ReadLine();  // Läs in spelarens val.

                // Försök att omvandla det inmatade värdet till ett nummer mellan 1 och 9.
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 9)
                {
                    // Här räknar vi ut vilken rad och kolumn som den valda siffran motsvarar.
                    int row = (choice - 1) / 3;
                    int col = (choice - 1) % 3;

                    // Kontrollera om rutan är tom. Om den är ledig, gör vi draget.
                    if (board[row, col] != 'X' && board[row, col] != 'O')
                    {
                        board[row, col] = currentPlayer;  // Spelaren markerar sin symbol i rutan.
                        validMove = true;  // Dra är nu giltigt.
                    }
                    else
                    {
                        // Om rutan redan är fylld, ber vi spelaren välja en annan ruta.
                        Console.WriteLine("Den rutan är redan tagen, försök en annan.");
                    }
                }
                else
                {
                    // Om spelaren inte skrev in ett nummer mellan 1 och 9, ber vi dem försöka igen.
                    Console.WriteLine("Oj, något gick fel. Skriv en siffra mellan 1 och 9.");
                }
            }
        }

        // Den här metoden byter spelare. Om det är 'X' som spelar nu, blir det 'O' nästa gång, och vice versa.
        static void SwitchPlayer()
        {
            // Vi växlar mellan 'X' och 'O' beroende på vem som spelar just nu.
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        // Den här metoden kollar om någon har vunnit. Vi kollar alla rader, kolumner och diagonaler.
        static bool CheckWinner()
        {
            // Kolla varje rad för att se om alla tre rutorna är lika.
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;  // Om alla tre rutorna i en rad är lika, så har någon vunnit.
            }

            // Kolla varje kolumn för att se om alla tre rutorna är lika.
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return true;  // Om alla tre rutorna i en kolumn är lika, så har någon vunnit.
            }

            // Kolla diagonalerna.
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;  // Om alla tre rutorna på vänstra diagonalen är lika.

            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return true;  // Om alla tre rutorna på högra diagonalen är lika.

            return false;  // Om ingen har vunnit än, returnera false.
        }

        // Den här metoden kollar om alla rutor på spelplanen är fyllda. Om det är det, så är spelet slut.
        static bool IsFull()
        {
            // Kollar alla rutor och ser om någon fortfarande är tom.
            foreach (char c in board)
            {
                if (c != 'X' && c != 'O')  // Om vi hittar en ruta som inte är fylld.
                    return false;  // Spelet är inte slut ännu.
            }

            return true;  // Om ingen ruta är tom, då är spelplanen full och spelet är slut.
        }
    }

}