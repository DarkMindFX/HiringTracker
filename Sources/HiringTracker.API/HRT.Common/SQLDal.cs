﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace HRT.Common
{
    public abstract class SQLDal
    {
        protected string connectionString;
        protected object ValueOrDBNull(object value)
        {
            return value != null ? value : DBNull.Value;
        }

        protected SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(this.connectionString);
            conn.Open();

            return conn;
        }

        protected void CloseConnection(SqlConnection conn)
        {
            conn.Close();
        }

        protected void InitDbConnection(string connString)
        {
            connectionString = connString;
        }

        protected SqlParameter AddParameter(SqlCommand cmd, string parameterName, SqlDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            SqlParameter p = new SqlParameter(parameterName, dbType, size, direction, isNullable, precision, scale, sourceColumn, sourceVersion, value);
            cmd.Parameters.Add(p);

            return p;
        }

        protected DataSet FillDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            da.Fill(ds);

            return ds;

        }
    }
}
