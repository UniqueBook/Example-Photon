using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;

namespace NHibernateMysql
{
    class NHibernateHelper
    {
        //构建 session工厂，会话 数据库连接
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory==null)
                {
                    //var  若类型 ，可代替任何类型，类似object 但比之效率更高
                    //必须在定义时初始化。也就是必须是var s = “abcd”形式
                    var configration = new Configuration();
                    configration.Configure();//解析hibernate.cfg.xml   会按照默认路径寻找该文件 ;也可自己定义指定文件名称
                    configration.AddAssembly("NHibernateMysql");//解析映射文件，User.hbm.xml等  映射文件已经嵌套在打包后的程序集

                    _sessionFactory = configration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            //获得session   打开一个跟数据库的会话
            return SessionFactory.OpenSession();
        }
    }
}
