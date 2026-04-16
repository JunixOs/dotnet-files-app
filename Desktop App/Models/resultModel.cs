using System;
using System.Collections.Generic;
using System.Text;

namespace Desktop_App.Models
{
    internal class ResultModel<T>
    {
        public T? Data;
        public string? Message;
        public bool Success;
    }
}
