using System;
using System.Collections.Generic;
using System.Text;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using System.IO;
using log4net.Config;
using MyPhoton.Handler;
using Common;
using MyPhoton.Threads;

namespace MyPhoton
{
    //所有的server类   都要继承自ApplicationBase
    public class MyServer : ApplicationBase
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public static MyServer _instance
        {
            get;
            private set;  //只允许内部赋值
        }

        //统一管理所有的 handler (客户端的请求)
        public Dictionary<OperationCode, BaseHandler> handlerDict = new Dictionary<OperationCode, BaseHandler>();

        //保存所有客户端的peer，当有需要可以从集合中得到所欲用户的peer，发送数据
        public List<MyClient> clientPeer = new List<MyClient>();

        //创建线程
        private SyncPositionThread syncPositionThread = new SyncPositionThread();

        //当一个客户端发起连接请求时
        //使用 peerBase 类型，表示和一个客户端连接
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("一个客户端连接过来了");
            MyClient peer = new MyClient(initRequest);
            clientPeer.Add(peer);
            return peer;
        }

        //服务器端应用启动时调用（初始化）
        protected override void Setup()
        {
            _instance = this;
            //设置  日志配置文件中的属性
            //Photon:ApplicationLogPath   配置文件日志所在的目录
            //ApplicationPath 应用所在的根目录

            //log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(Path.Combine(this.ApplicationRootPath, "bin_win64"), "log");
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            log4net.GlobalContext.Properties["LogFileName"] = "MyPhotonLog";   //配置文件日志的命名

            //日志的初始化   this.BinaryPath  获取部署目录路径（bin目录）  配置文件所在的目录
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);//让photon知道
                XmlConfigurator.ConfigureAndWatch(configFileInfo);//让log4net这个插件读取配置
            }
            log.Info("初始化完成！");
            InitHandler();

            //启动线程
            syncPositionThread.Run();
        }

        //初始化创建 loginHandler
        public void InitHandler()
        {
            LoginHandler loginHandler = new LoginHandler();
            handlerDict.Add(loginHandler.OpCode, loginHandler);
            DefaultHandler defaultHandler = new DefaultHandler();
            handlerDict.Add(defaultHandler.OpCode, defaultHandler);
            RegisterHandler registerHandler = new RegisterHandler();
            handlerDict.Add(registerHandler.OpCode, registerHandler);
            SyncPositionHandler syncPositionHandler = new SyncPositionHandler();
            handlerDict.Add(syncPositionHandler.OpCode, syncPositionHandler);
            SyncPlayerHandler syncPlayerHandler = new SyncPlayerHandler();
            handlerDict.Add(syncPlayerHandler.OpCode, syncPlayerHandler);
        }

        //server 端关闭的时候所做的一些处理
        protected override void TearDown()
        {
            log.Info("服务器关闭了！");
            //关闭线程
            syncPositionThread.Stop();
        }
    }
}