using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace PgSqlBrowser
{
    public class peDAC
    {
        
        public delegate void connectionAttemptStatus(string ConnStatus, EventArgs a);
        public event connectionAttemptStatus checkStatus;

        public NpgsqlConnection conn = null;
        private string _serverName;
        private string _databaseName;
        private string _userName;
        private string _pWord;
        public string serverName {get {return _serverName;}}
        public string databaseName{get {return _databaseName;}}
        public string userName {get {return _userName;}}
        public string pWord { get { return _pWord; } }
        public string error_string = "";

        public peDAC()
        {
            //blank
        }

        public peDAC(string ServerName, string DatabaseName, string UserName, string PWord)
        {
            MakeConnection(ServerName, DatabaseName, UserName, PWord);
        }

        public NpgsqlConnection MakeConnection(string ServerName = null, string DatabaseName = null, string UserName = null, string PWord = null)
        {
            _serverName = ServerName;
            _databaseName = DatabaseName;
            _userName = UserName;
            _pWord = PWord;

            string connString = "Host=" + _serverName + ";Database="+_databaseName+";Username="+_userName+";Password="+_pWord;
            conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                error_string = e.Message.ToString();
            }
            checkStatus(conn.State.ToString() + " :: " + error_string, null);
            return conn;
        }
    }
}
