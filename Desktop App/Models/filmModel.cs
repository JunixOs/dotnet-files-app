using System;
using System.Collections.Generic;
using System.Text;

namespace Desktop_App.Models
{
    // "internal class" sirve para limitar la visibilidad de la clase
    // solamente a este proyecto
    // es decir, no se puede ver desde afuera
    internal class FilmModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateOnly PremierDate { get; set; }
    }
}
