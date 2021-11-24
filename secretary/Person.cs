using System;
using System.Collections.Generic;
using System.Text;
//przedmioty nauczane lista
// 
namespace secretary
{
    public abstract class Person
    {
        public string name;
        public string secondName;
        public string surname;
        public string maidenName;
        public string fathersName;
        public string mothersName;
        public DateTime birthDate;
        public string[] pesel= new string[11];
        public string imagePath;
        public char gender;
        
    }
}
