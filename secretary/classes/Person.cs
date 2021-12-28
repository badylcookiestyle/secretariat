using System;
using System.Collections.Generic;
using System.Text;
//przedmioty nauczane lista
// 
namespace secretary
{
    public abstract class Person
    {
        public char personType;
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string lastname { get; set; }
        public string maidenName { get; set; }
        public string fathersName { get; set; }
        public string mothersName { get; set; }
        public DateTime birthDate { get; set; }
        public string pesel;
        public string imagePath;
        public string gender;
        
    }
}
