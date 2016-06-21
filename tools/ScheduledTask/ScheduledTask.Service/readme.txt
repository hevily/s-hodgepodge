1、安装服务
1）用管理员身份打开命令提示符
2）执行命令 cd "path"，定位到服务安装目录
3）执行命令 installutil ScheduledTask.Service.exe，安装服务

2、卸载服务
1）用管理员身份打开命令提示符
2）执行命令 cd "path"，定位到服务安装目录
3）执行命令 installutil /u ScheduledTask.Service.exe，卸载服务

3、重启服务
1）用管理员身份打开命令提示符
2）net stop ScheduledTaskService
3）net start ScheduledTaskService