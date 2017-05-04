using group.cfm.core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group.cfm.core.examples
{
    public abstract class DBBase<T> where T : DBBase<T>, new()
    {
        #region Fields
        protected abstract String TABLENAME { get; }


        protected static string DB_ID = "ID";
        protected static string DB_CREATEDBY = "createdBy";
        protected static string DB_CREATEDON = "createdOn";
        protected static string DB_UPDATEDBY = "updatedBy";
        protected static string DB_UPDATEDON = "updatedOn";
        protected static string DB_OBJECTDATA = "objectdata";

        protected long _Id;
        protected long _CreatedBy;
        protected DateTimeOffset _CreatedOn;
        protected long _UpdatedBy;
        protected DateTimeOffset _UpdatedOn;
        protected string _ObjectData;

        public long Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }
        public long CreatedBy
        {
            get
            {
                return _CreatedBy;
            }

            set
            {
                _CreatedBy = value;
            }
        }
        public DateTimeOffset CreatedOn
        {
            get
            {
                return _CreatedOn;
            }

            set
            {
                _CreatedOn = value;
            }
        }
        public long UpdatedBy
        {
            get
            {
                return _UpdatedBy;
            }

            set
            {
                _UpdatedBy = value;
            }
        }
        public DateTimeOffset UpdatedOn
        {
            get
            {
                return _UpdatedOn;
            }

            set
            {
                _UpdatedOn = value;
            }
        }
        protected string ObjectData
        {
            get
            {
                return _ObjectData;
            }
        }

        #endregion

        protected T Load(SqlDataReader data)
        {
            _ObjectData = data.ParseValue<string>(DB_OBJECTDATA);
            Newtonsoft.Json.JsonConvert.PopulateObject(_ObjectData, this);
            _Id = data.ParseValue<long>(DB_ID);
            _CreatedBy = data.ParseValue<long>(DB_CREATEDBY);
            _CreatedOn = (DateTimeOffset)data.ParseValue<DateTime>(DB_CREATEDON);
            _UpdatedBy = data.ParseValue<long>(DB_UPDATEDBY);
            _UpdatedOn = (DateTimeOffset)data.ParseValue<DateTime>(DB_UPDATEDON);
            return ImplementLoad(data);
        }

        public static T ById(long id, SqlCommand cmd = null)
        {
            var cmd1 = cmd ?? DBHelper.Command;
            SqlDataReader data = null;
            T res = null;
            try
            {
                res = new T();
                cmd1.CommandText = String.Format("SELECT * FROM [{0}].[{1}] WHERE {2} = @id ", DBHelper.Scheme, res.TABLENAME, DB_ID);
                cmd1.Parameters.Clear();
                cmd1.Parameters.AddWithValue("id", id);
                data = cmd1.ExecuteReader();
                if (data.Read())
                {
                    res.Load(data);
                }
            }
            catch (Exception e)
            {
                // Error handling / logging
                return null;
            }
            finally
            {
                if (data != null) data.Close();
                if (cmd == null) cmd1.Connection.Close();
            }
            return res;
        }

        protected void LogEvent(string eventname, Exception e = null)
        {
            // Implement the log, add details to function if and when required
        }

        public void Delete(SqlCommand cmd = null)
        {
            var cmd1 = cmd ?? DBHelper.Command;
            try
            {
                cmd1.CommandText = String.Format("DELETE FROM [{0}].[{1}] WHERE {2} = @id ", DBHelper.Scheme, TABLENAME, DB_ID);
                cmd1.Parameters.Clear();
                cmd1.Parameters.AddWithValue("id", _Id);
                cmd1.ExecuteNonQuery();
                LogEvent("Deleted");
            }
            catch (Exception e)
            {
                // Error handling / logging
                return;
            }
            finally
            {
                if (cmd == null) cmd1.Connection.Close();
            }
        }

        public void Save(SqlCommand cmd = null)
        {
            var cmd1 = cmd ?? DBHelper.Command;
            if (!ValidateBeforeSave(cmd1)) return;
            try
            {
                var fldMap = GetFieldValueMap();
                _ObjectData = Newtonsoft.Json.JsonConvert.SerializeObject(this);
                if (_Id == -1 )
                {
                    // INSERT
                    cmd1.CommandText = String.Format("INSERT INTO [{0}].[{1}] ([{4}], [{5}], [{6}], [{7}], [{8}]{2}) VALUES (@CreatedBy, @CreatedOn, @UpdatedBy, @UpdatedOn, @ObjectData{3}); SELECT @@IDENTITY;",
                        DBHelper.Scheme, TABLENAME,
                        fldMap.Keys.Select(x => String.Format(",{0}", x)),
                        fldMap.Keys.Select(x => String.Format(",@{0}", x)),
                        DB_CREATEDBY, DB_CREATEDON, DB_UPDATEDBY, DB_UPDATEDON, DB_OBJECTDATA
                        );
                    cmd1.Parameters.Clear();
                    cmd1.Parameters.AddWithValue("CreatedBy", UserFactory.CurrentUserDetails().GetUniqueId());
                    cmd1.Parameters.AddWithValue("CreatedOn", DateTime.Now);
                    cmd1.Parameters.AddWithValue("UpdatedBy", UserFactory.CurrentUserDetails().GetUniqueId());
                    cmd1.Parameters.AddWithValue("UpdatedOn", DateTime.Now);
                    cmd1.Parameters.AddWithValue("ObjectData", _ObjectData);
                    foreach (var kvp in fldMap)
                        cmd1.Parameters.AddWithValue(kvp.Key, kvp.Value);
                    _Id = (long)cmd1.ExecuteScalar();
                    LogEvent("INSERT");
                }
                else
                {
                    // UDPATE
                    cmd1.CommandText = String.Format("UPDATE [{0}].[{1}] SET [{3}] = @UpdatedBy, [{4}] = @UpdatedOn, [{6}] = @ObjectData{2} WHERE [{5}] = @Id",
                        DBHelper.Scheme, TABLENAME,
                        fldMap.Keys.Select(x => String.Format(",[{0}] = @{0}", x)),
                        DB_UPDATEDBY, DB_UPDATEDON, DB_ID, DB_OBJECTDATA
                        );
                    cmd1.Parameters.Clear();
                    cmd1.Parameters.AddWithValue("UpdatedBy", UserFactory.CurrentUserDetails().GetUniqueId());
                    cmd1.Parameters.AddWithValue("UpdatedOn", DateTime.Now);
                    cmd1.Parameters.AddWithValue("ObjectData", _ObjectData);
                    foreach (var kvp in fldMap)
                        cmd1.Parameters.AddWithValue(kvp.Key, kvp.Value);
                    _Id = (long)cmd1.ExecuteScalar();
                    LogEvent("UPDATE");
                }
                PostProcessSave(cmd1);
            }
            catch (Exception e)
            {
                // Error handling / logging
                LogEvent("Save", e);
                return;
            }
            finally
            {
                if (cmd == null) cmd1.Connection.Close();
            }
        }


        protected abstract bool ValidateBeforeSave(SqlCommand cmd);
        protected abstract void PostProcessSave(SqlCommand cmd);

        protected abstract T ImplementLoad(SqlDataReader data);

        protected abstract Dictionary<string, object> GetFieldValueMap();
    }
}
