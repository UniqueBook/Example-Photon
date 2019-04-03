using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPhoton.Model;
using NHibernate;
using NHibernate.Criterion;

namespace MyPhoton.Manger
{
    class UserManager : IUserManager
    {
        private static ISession session = null;

        private static ITransaction transaction = null;

        private static User user = null;

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {
            // 方式一：  需手动关闭资源
            //ISession session = NHibernateHelper.OpenSession();
            //session.Save(user);
            //session.Close();

            // 方式二：  自动关闭资源
            using (session = NHibernateHelper.OpenSession())
            {
                using (transaction = session.BeginTransaction()) //开启事务
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns>User</returns>
        public ICollection<User> GetAllUsers()
        {
            using (session = NHibernateHelper.OpenSession())
            {
                //指定要查询的表
                ICriteria criteria = session.CreateCriteria(typeof(User));
                IList<User> users = criteria.List<User>();

                return users;
            }
        }

        /// <summary>
        /// 通过 id 查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            using (session = NHibernateHelper.OpenSession())
            {
                user = session.Get<User>(id);
                return user;
            }
        }

        /// <summary>
        /// 通过用户昵称查询用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public User GetByName(string name)
        {
            using (session = NHibernateHelper.OpenSession())
            {

                //指定要查询的表
                //ICriteria criteria = session.CreateCriteria(typeof(User));
                //criteria.Add(Restrictions.Eq("Name", name));
                //User user = criteria.UniqueResult<User>();

                User user = session.CreateCriteria(typeof(User)).Add(Restrictions.Eq("Name", name)).UniqueResult<User>();
                return user;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        public void Remove(User user)
        {
            using (session = NHibernateHelper.OpenSession())
            {
                using (transaction = session.BeginTransaction())
                {
                    session.Delete(user);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// 修改用户 （必须带有主键）
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            using (session = NHibernateHelper.OpenSession())
            {
                using (transaction = session.BeginTransaction())
                {
                    session.Update(user);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool VerifyUser(string name, string pwd)
        {
            using (session = NHibernateHelper.OpenSession())
            {
                User user = session.CreateCriteria(typeof(User))
                    .Add(Restrictions.Eq("Name", name))
                    .Add(Restrictions.Eq("Pwd", pwd))
                    .UniqueResult<User>();
                if (user == null) return false;
                return true;
            }
        }
    }
}
