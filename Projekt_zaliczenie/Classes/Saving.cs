using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projekt_zaliczenie.Classes
{
    public static class Saving
    {

        public static void Mail_Save(string sender, string receiver, string theme, string con)
        {

            string docPath =
                 Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"{theme}.txt")))
            {
                outputFile.WriteLine($"Nadawca: {sender}");
                outputFile.WriteLine($"Odbiorca: {receiver}");
                outputFile.WriteLine($"Temat {theme}");
                outputFile.WriteLine();
                outputFile.WriteLine($"Treść: {con}");
            }
        }


    }
}
