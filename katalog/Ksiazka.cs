using System;
using System.Collections.Generic;
using System.Text;

namespace katalog
{
    class Ksiazka
    {
        public int numer_seryjny { get; set; }
        public string Tytul { get; set; }
        public string Autor { get; set; }
        public string Gatunek { get; set; }

        public Ksiazka(int isbn, string Tyt, string Aut, string gat)
        {
            numer_seryjny = isbn;
            Tytul = Tyt;
            Autor = Aut;
            Gatunek = gat;
        }
    }

}
