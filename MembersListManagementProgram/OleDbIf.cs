using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace MembersListManagementProgram
{
    class OleDbIf : IDisposable
    {
        /// <summary>
        /// SQLコネクション
        /// </summary>
        private OleDbConnection _conn = null;

        /// <summary>
        /// トランザクション・オブジェクト
        /// </summary>
        /// <remarks></remarks>
        private OleDbTransaction _trn = null;

        /// <summary>
        /// DB接続
        /// </summary>
        public void Connect()
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
        public void Disconnect()
        {
            try
            {
                _conn.Close();
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
        public DataTable ExecuteSql(String strSql)
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbCommand cmd = new OleDbCommand(strSql, _conn, _trn);
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
        public void BeginTransaction()
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
        public void CommitTransaction()
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
        public void RollbackTransaction()
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
        /// オブジェクトを破棄
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// オブジェクトを破棄
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 管理（managed）リソースの破棄処理をここに記述します。 
            }
            // 非管理（unmanaged）リソースの破棄処理をここに記述します。
            Disconnect();
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        /// <remarks></remarks>
        ~OleDbIf()
        {
            Dispose();
        }
    }
}
