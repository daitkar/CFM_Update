using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group.cfm.core.examples
{
    public class Question : DBBase<Question>
    {
        #region Fields
        private static string DB_TABLE = "QUESTIONS";
        private static string DB_NAME = "name";
        private static string DB_DESCRIPTION = "description";
        private static string DB_TYPE = "type";

        private String _Name;
        private String _Description;
        private Int64 _Type;

        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }
        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
            }
        }
        public long Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = value;
            }
        }
        public string BlaBla { get; set; }

        protected override string TABLENAME
        {
            get
            {
                return DB_TABLE;
            }
        }

        #endregion


        public static List<Question> GetList(SqlCommand cmd = null)
        {
            var cmd1 = cmd ?? DBHelper.Command;
            SqlDataReader data = null;
            List<Question> res = new List<Question>();
            try
            {
                cmd1.CommandText = String.Format("SELECT * FROM [{0}].[{1}] ORDER BY {2} ASC", DBHelper.Scheme, DB_TABLE, DB_DESCRIPTION);
                data = cmd1.ExecuteReader();
                while (data.Read())
                {
                    res.Add(new Question().Load(data));
                }
            }
            catch (Exception e)
            {
                // Error handling and logging
            }
            finally
            {
                if (data != null) data.Close();
                if (cmd == null) cmd1.Connection.Close();
            }
            return res;
        }

        protected override Question ImplementLoad(SqlDataReader data)
        {
            _Name = data.ParseValue<string>(DB_NAME);
            _Description = data.ParseValue<string>(DB_DESCRIPTION);
            _Type = data.ParseValue<long>(DB_TYPE);
            return this;
        }

        protected override Dictionary<string, object> GetFieldValueMap()
        {
            var map = new Dictionary<string, object>();
            map.Add(DB_NAME, _Name);
            map.Add(DB_DESCRIPTION, _Description);
            map.Add(DB_TYPE, _Type);
            return map;
        }

        protected override bool ValidateBeforeSave(SqlCommand cmd)
        {
            return !String.IsNullOrWhiteSpace(_Name);
        }

        protected override void PostProcessSave(SqlCommand cmd)
        {
            // No post process

        }
    }
}
