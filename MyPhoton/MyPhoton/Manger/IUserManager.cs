using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPhoton.Model;

namespace MyPhoton.Manger
{
    interface IUserManager
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        void Add(User user);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        void Update(User user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        void Remove(User user);

        /// <summary>
        /// 通过id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetById(int id);

        /// <summary>
        /// 通过用户昵称查询用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        User GetByName(string name);

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        ICollection<User> GetAllUsers();

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        bool VerifyUser(string name, string pwd);
    }
}
