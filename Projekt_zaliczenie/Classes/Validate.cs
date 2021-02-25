using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_zaliczenie.Classes
{
    /// <summary>
    /// Klasa metod Formatujących tekst do akceptowalnych wartosci.
    /// </summary>
    public static class Validate
    {
        /// <summary>
        /// Metoda Formatujaca numer telefonu.
        /// </summary>
        /// <param name="number">Numer do sformatowania( string )</param>
        /// <param name="country">Nazwa kraju</param>
        /// <returns></returns>
        public static string Number(string number, string country)
        {
            string temp = number.Replace(" ", "");
            string[] split = number.Split();
            StringBuilder sb = new StringBuilder();

            if (country == "Poland")
            {
                if (temp.Length == 9)
                {
                    foreach (var item in split)
                    {
                        if (item.Trim() != "")
                        {
                            sb.Append(item);
                        }
                    }
                    return String.Format("+48 {0:000 000 000}", double.Parse(sb.ToString()));
                }
            }

            if (country == "Germany")
            {
                if (temp.Length == 11)
                {
                    foreach (var item in split)
                    {
                        if (item.Trim() != "")
                        {
                            sb.Append(item);
                        }
                    }
                    return String.Format("+49 {0:000 000 00000}", double.Parse(sb.ToString()));
                }
            }

            if (country == "Norway")
            {
                if (temp.Length == 8)
                {
                    foreach (var item in split)
                    {
                        if (item.Trim() != "")
                        {
                            sb.Append(item);
                        }
                    }
                    return String.Format("+47 {0:00 00 00 00}", double.Parse(sb.ToString()));


                }
            }

            if (country == "Czech")
            {
                if (temp.Length == 9)
                {
                    foreach (var item in split)
                    {
                        if (item.Trim() != "")
                        {
                            sb.Append(item);
                        }
                    }
                    return String.Format("+420 {0:000 000 000}", double.Parse(sb.ToString()));
                }
            }

            return number;
        }
        /// <summary>
        /// Metoda usuwająca niepotrzebne spacje pomiędzy imieniem i nazwiskiem.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Name(string name)
        {
            if (name.Length > 2)
            {
                if (name[name.Length - 1].ToString() == " " && name[name.Length - 2].ToString() == " ")
                {
                    string[] spl = name.Split(' ');
                    string temp = "";

                    for (int i = 0; i < spl.Length; i++)
                    {
                        temp += spl[i];
                    }

                    return temp;
                }
            }
            return name;
        }

    }
}
