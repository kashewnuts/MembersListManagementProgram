using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    class OleDbIf
    {
        private OleDbConnection _conn = null;
        private OleDbTransaction _trn = null;

        /// <summary>
        /// DB接続
        /// </summary>
        public void connect()
        {
            try
            {
                if (_conn == null)
                {
                    _conn = new OleDbConnection();
                }
                _conn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;
                _conn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Connect Error", ex);
            }
        }

        /// <summary>
        /// DB切断
        /// </summary>
        public void disconnect()
        {
            try
            {
                if (_conn != null) _conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Disconnect Error", ex);
            }
        }

        /// <summary>
        /// SQL実行
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataTable executeSql(String strSql)
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbCommand cmd = new OleDbCommand(strSql, _conn);
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(dt);
                adp.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteSql Error", ex);
            }

            return dt;
        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        /// <remarks></remarks>
        public void beginTransaction()
        {
            try
            {
                _trn = _conn.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception("BeginTransaction Error", ex);
            }
        }

        /// <summary>
        /// コミット
        /// </summary>
        /// <remarks></remarks>
        public void commitTransaction()
        {
            try
            {
                if (_trn != null)
                {
                    _trn.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CommitTransaction Error", ex);
            }
            finally
            {
                _trn = null;
            }
        }

        /// <summary>
        /// ロールバック
        /// </summary>
        /// <remarks></remarks>
        public void rollbackTransaction()
        {
            try
            {
                if (_trn != null)
                {
                    _trn.Rollback();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RollbackTransaction Error", ex);
            }
            finally
            {
                _trn = null;
            }
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        /// <remarks></remarks>
        ~OleDbIf()
        {
            disconnect();
        }
    }
}
