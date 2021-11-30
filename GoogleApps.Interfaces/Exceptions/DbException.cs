using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleApps.Interfaces.Exceptions
{
    public class DbException:Exception
    {
        public DbException(string message): base(message)
        {
        }
        public DbException()
        {

        }
    }
}
