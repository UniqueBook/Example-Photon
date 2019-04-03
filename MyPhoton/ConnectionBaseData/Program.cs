using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConnectionBaseData
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(ReadData("book", "123"));
        }

        static bool ReadData(string name, string pwd)
        {
            string mysql = "server=127.0.0.1;port=3306;database=myphoton;user=root;password=ushen@lsh";
            MySqlConnection conn = new MySqlConnection(mysql);
            try
            {
                //打开连接数据库
                conn.Open();
                string sql = "select * from user where name =@pra1 and pwd =@pra2";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("pra1", name);
                cmd.Parameters.AddWithValue("pra2", pwd);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
            }
            catch (Exception exception)
            {

                throw exception;
            }
            finally
            {
                //关闭连接
                conn.Close();
            }
            return false;
        }
    }
}
