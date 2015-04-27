using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Users
{
    public class UserManager
    {
        // Private
        private User[] _users;
        private ILocalUser[] _localUsers;
        private IWebUser[] _webUsers;

        private int _maxUsers;
        private int _usersCount;

        // Public
        public int MaxUsers
        { get { return _maxUsers; } }
        public int UserCount
        { get { return _usersCount; } }

        // Constructor(s)
        internal UserManager(int maxUsers)
        {
            _maxUsers = maxUsers;

            _users = new User[maxUsers];
            _localUsers = new ILocalUser[maxUsers];
            _webUsers = new IWebUser[maxUsers];
        }

        // Users
        public void AddGuest()
        {

        }

        public bool AddUser(User user)
        {
            //
            if (_users.Contains(user))
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to add already contained user", this.GetType().Name));
                return false;
            }

            //
            if (_usersCount >= _maxUsers)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to add user - UserContainer is full", this.GetType().Name));
                return false;
            }

            //
            int length = _maxUsers;
            for (int i = 0; i < _maxUsers; i++)
            {
                //
                if (_users[i] != null)
                    continue;

                //
                _users[i] = user;

                //
                if (user is ILocalUser)
                _localUsers[i] = (ILocalUser)user;
                if (user is IWebUser)
                    _webUsers[i] = (IWebUser)user;

                //
                _usersCount++;

                //
                return true;
            }

            //
            return false;
        }
        public bool RemoveUser(User user)
        {
            //
            if (!_users.Contains(user))
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove non-contained user", this.GetType().Name));
                return false;
            }

            //
            if (_usersCount == 0)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove user - UserContainer is empty", this.GetType().Name));
                return false;
            }

            //
            int length = _maxUsers;
            for (int i = 0; i < _maxUsers; i++)
            {
                //
                if (_users[i] != user)
                    continue;

                //
                _users[i] = null;

                //
                if (user is ILocalUser)
                    _localUsers[i] = null;
                if (user is IWebUser)
                    _webUsers[i] = null;

                //
                _usersCount--;

                //
                return true;
            }

            //
            return false;
        }
    }
}
