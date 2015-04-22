using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIB.Training.BusinessComponets
{
    public class DatabaseOperate
    {
        public static int[] GetFirst()
        {
            int[] data = new int[2];
            DataSet dataSet = null;
            string select = "select top 1 * from BAS_TODOITEM";
            dataSet = GetDataSet(select, "BAS_TODOITEM");
            data[0] = (Int32)dataSet.Tables[0].Rows[0][0];
            data[1] = (Int32)dataSet.Tables[0].Rows[0][1];

            return data;
        }

        public static int[] GetLast()
        {
            int[] data = new int[2];
            DataSet dataSet = null;
            string select = "select top 1 * from BAS_TODOITEM order by SORTING DESC";
            dataSet = GetDataSet(select, "BAS_TODOITEM");
            data[0] = (Int32)dataSet.Tables[0].Rows[0][0];
            data[1] = (Int32)dataSet.Tables[0].Rows[0][1];

            return data;
        }

        public static ArrayList GetById(int id)
        {
            ArrayList data = new ArrayList();
            string select = "select * from BAS_TODOITEM where ID = @ID";
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(select, conn))
                {
                    SqlParameter sp = new SqlParameter("@ID", id);
                    cmd.Parameters.Add(sp);
                    SqlDataReader reader = null;
                    try
                    {
                        conn.Open();
                        reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                data.Add(reader[i]);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        reader.Close();
                        conn.Close();
                        cmd.Clone();
                    }
                }
            }

            return data;
        }

        public static int GetAll(string tableName)
        {
            int count = 0;
            string select = "select * from " + tableName;

            //创建连接
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                //创建执行命令
                using (SqlCommand cmd = new SqlCommand(select, conn))
                {
                    SqlDataReader reader = null;
                    try
                    {
                        conn.Open();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            count++;
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        reader.Close();
                        conn.Close();
                        cmd.Clone();
                    }
                }
            }
            
            return count;
        }

        public static DataSet GetDefault(int isDefault)
        {
            DataSet dataSet = null;
            string select = "select * from BAS_TODOITEM where ISDEFAULT = " + isDefault;
            dataSet = GetDataSet(select, "BAS_TODOITEM");

            return dataSet;
        }

        public static bool GetSave(int id, int version, DateTime dateTime)
        {
            string sqlText = "UPDATE BAS_TODOITEM SET VERSION = @VERSION,UPDATEDDATE = @UPDATEDDATE WHERE ID = @ID";
            
            //创建连接
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                //创建执行命令
                using (SqlCommand cmd = new SqlCommand(sqlText, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    cmd.Parameters.Add(new SqlParameter("@VERSION", version));
                    cmd.Parameters.Add(new SqlParameter("@UPDATEDDATE", dateTime));
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                        cmd.Clone();
                    }
                }
            }

            return true;
        }

        public static bool GetCreate(int id, int sorting,
            string description, int isDefault, DateTime
            createtime, DateTime updatetime, int version)
        {
            string sqlText = "INSERT INTO BAS_TODOITEM(ID, SORTING, DESCRIPTION, ISDEFAULT, CREATEDDATE, UPDATEDDATE,VERSION) VALUES(@ID, @SORTING, @DESCRIPTION, @ISDEFAULT, @CREATEDDATE, @UPDATEDDATE, @VERSION)";
            //创建连接
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                //创建执行命令
                using (SqlCommand cmd = new SqlCommand(sqlText, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    cmd.Parameters.Add(new SqlParameter("@SORTING", sorting));
                    cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", description));
                    cmd.Parameters.Add(new SqlParameter("@ISDEFAULT", isDefault));
                    cmd.Parameters.Add(new SqlParameter("@CREATEDDATE", createtime));
                    cmd.Parameters.Add(new SqlParameter("@UPDATEDDATE", updatetime));
                    cmd.Parameters.Add(new SqlParameter("@VERSION", version));
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                        cmd.Clone();
                    }
                }
            }

            return true;
        }

        public static bool GetDelete(int id)
        {
            string sqlText = " DELETE FROM BAS_TODOITEM WHERE ID = @ID";

            //创建连接
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                //创建执行命令
                using (SqlCommand cmd = new SqlCommand(sqlText, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                        cmd.Clone();
                    }
                }
            }

            return true;
        }

        public static DataSet GetSearch(string description)
        {
            string sqlText = "select * from BAS_TODOITEM where DESCRIPTION like " + "'%" + description + "%'";
            DataSet dataSet = null;
            dataSet = GetDataSet(sqlText, "BAS_TODOITEM");

            return dataSet;
        }

        static string GetDatabaseConnection()
        {
            string ConnectionString = "server=(local);" +
                "integrated security=SSPI;" +
                "database=Training";
            
            return ConnectionString;

            //string ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=G:\RIB.Training.BusinessComponets\RIB.Training.BusinessComponets\bin\Debug\database\Training.mdf;Integrated Security=True;Connect Timeout=30";
            //return ConnectionString;
        }

        public static DataSet GetDataSet(string sql, string tableName)
        {
            DataSet dataSet = new DataSet();

            //创建连接
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                //创建SqlDataAdapter
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn))
                {
                    try
                    {
                        dataAdapter.Fill(dataSet, tableName);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                        dataAdapter.Dispose();
                    }
                }
            }

            return dataSet;
        }

        public static int GetCount(string sql)
        {
            int count = 0;

            //创建连接
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                //创建执行命令
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        conn.Open();
                        object o = cmd.ExecuteScalar();
                        count = Convert.ToInt32(o); ;
                    }
                    catch( SqlException ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                        conn.Close();
                        cmd.Clone();
                    }

                }
            }

            return count;
        }
    }
}
