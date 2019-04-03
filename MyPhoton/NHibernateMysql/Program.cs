using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernateMysql.Model;
using NHibernateMysql.Manger;

namespace NHibernateMysql
{
    class Program
    {
        static void Main(string[] args)
        {
            //var  若类型 ，可代替任何类型，类似object 但比之效率更高
            //必须在定义时初始化。也就是必须是var s = “abcd”形式
            //var configration = new Configuration();
            //configration.Configure();//解析hibernate.cfg.xml   会按照默认路径寻找该文件 ;也可自己定义指定文件名称
            //configration.AddAssembly("NHibernateMysql");//解析映射文件，User.hbm.xml等  映射文件已经嵌套在打包后的程序集

            ////构建 session工厂，会话 数据库连接
            //ISessionFactory sessionFactory = null;
            ////获得session   打开一个跟数据库的会话
            //ISession session = null;
            ////事务  对数据库进行更改的时候使用
            ////在事务中进行的操作  有任意出错  会对数据进行回滚
            //ITransaction transaction = null;
            //try
            //{
            //    sessionFactory = configration.BuildSessionFactory();
            //    //获得session   打开一个跟数据库的会话
            //    session = sessionFactory.OpenSession();

            //    //User user = new User() { Name = "ming", Account = "123456", Pwd = "123" };
            //    //session.Save(user);

            //    transaction = session.BeginTransaction(); //开启事务   默认不开启
            //    //进行操作
            //    User user1 = new User() { Name = "ming16", Account = "123456", Pwd = "123" };
            //    User user2 = new User() { Name = "ming16", Account = "123456", Pwd = "123" };
            //    session.Save(user1);
            //    session.Save(user2);

            //    //进行提交
            //    transaction.Commit();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    //throw e;
            //}
            //finally
            //{
            //    if (transaction != null)
            //    {
            //        transaction.Dispose();//释放
            //    }

            //    if (session != null)
            //    {
            //        session.Close();
            //    }
            //    if (sessionFactory != null)
            //    {
            //        sessionFactory.Close();
            //    }
            //}
            //User user = new User() { Id = 2 };

            //IUserManager manager = new UserManager();
            UserManager manager = new UserManager();
            //manager.Add(user);
            //manager.Update(user);
            //manager.Remove(user);
            //User user = manager.GetById(7);
            //User user = manager.GetByName("小红");
            //ICollection<User> users =  manager.GetAllUsers();
            //foreach (var item in users)
            //{
            //    Console.WriteLine(item.Name);
            //}

            bool b = manager.VerifyUser("小红", "1322");
            Console.WriteLine(b);

            Console.ReadKey();//程序执行到此处暂停
        }
    }
}
