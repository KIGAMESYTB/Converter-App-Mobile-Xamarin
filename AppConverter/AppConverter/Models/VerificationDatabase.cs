using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppConverter.Models
{
    /// <summary>
    /// Table Database Verification First opening
    /// </summary>
    public class VerificationDatabase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool CreateDatabase { get; set; }

    }
}
