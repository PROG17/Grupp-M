using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    static class GFXText
    {
        // En metod för att skriva ut en sträng bokstav för bokstav, tar in position i sida, höjd, antalet tecken som ska skrivas ut innan rätt tecken skrivs ut (anges med ett negativt tal), fördröjning mellan varje tecken, strängen som ska skrivas ut, om markören ska skrivas, om mellanslag också ska fördröjas
        public static void PrintTxt(int xpos, int ypos, int trail, int delay, string inputString, bool cursor, bool forceTyping)
        {
            if (!cursor) Console.CursorVisible = false;
            else Console.CursorVisible = true;
            for (int i = 0; i < inputString.Length; i++)        // Gå igenom strängen tecken för tecken
            {
                if (inputString[i] != 32 || forceTyping)        // Hoppa över mellanslag och även dess position i sidled, såvida det inte ska hoppas över
                {
                    for (int i2 = trail; i2 < 1; i2++)          // Körs bara om 'trail' är mindre än 1, dvs är 'trail' 0 körs loopen en gång och rätt tecken skrivs direkt, annars kommer fel tecken att skrivas tills dess att rätt tecken skrivs ut
                    {
                        if (xpos == -1) Console.SetCursorPosition((Console.WindowWidth - inputString.Length) / 2 + i + 1, ypos);    // Är xpos satt till -1 centreras texten på skärmen i sidled och börjar halva strängens längd till vänster från mitten av skärmen
                        else Console.SetCursorPosition(xpos + i + 1, ypos);                                                         // Annars hamnar börjar den där xpos anger
                        Console.Write((char)(inputString[i] + i2));                                                                 // Skriv ut tecken, förskjutet uppåt i ASCII om 'i2' inte är 0
                        System.Threading.Thread.Sleep(delay);                                                                       // Pausa i angiven tid
                    }
                }
            }
        }

        public static void PrintTextWithHighlights(string text, int xPos, int yPos, bool fancyTyping)
        {
            int currentXPos=xPos;
            foreach (char tecken in text)
            {
                if (tecken == '[')
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    continue;
                }
                else if (tecken == ']')
                {
                    Console.ResetColor();
                    continue;
                }
                if (currentXPos >= LoadGame.windowWidth-2)
                {
                    Console.Write("-");
                    currentXPos = xPos;
                    yPos++;
                }
                Console.SetCursorPosition(currentXPos, yPos);

                if(fancyTyping) PrintTxt(currentXPos, yPos, -3, 5, tecken.ToString(), false, false);
                else Console.Write(tecken);

                currentXPos++;
            }            
        }
    }
}