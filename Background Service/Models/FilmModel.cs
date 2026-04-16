using System;
using System.Collections.Generic;
using System.Text;

namespace Background_Service.Models
{
    internal class FilmModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateOnly PremierDate { get; set; }
    }
}
