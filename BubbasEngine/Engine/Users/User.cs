using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Users
{
    public abstract class User
    {
        // Private
        private string _name;

        // Public
        public string Name
        { get { return _name; } set { _name = value; } }

        // Constructor(s)
        protected User()
        {
            _name = "";
        }
    }
}
