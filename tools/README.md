# tools
自己开发的一些小工具。

## DomainScanner
域名扫描工具，基于`nodejs`，调用阿里云的域名注册查询 api，可以用来扫描指定位数的域名是否已被注册。（很遗憾，所有3位、4位的后缀为 `.com` `.net` 的域名都被注册光了）

## ThsReptile
同花顺模拟炒股爬虫，基于`nodejs`，扫描“总盈利率排名”，“月盈利率排名”，“周盈利率排名”，“日盈利率排名”，“选股成功率排名”，“被追踪数排名”，“段位排名”七大榜单前一百名用户当日是否操作。

## FileFileRegularReplace
文件正则替换工具，基于`.NET`，用正则来批量处理文件夹里的所有文本（递归子目录）。

## FolderComparator
文件夹比较工具，基于`.NET`，用来比较两个文件夹中有哪些文件不一样。

## IISJournalAnalyzer
IIS日志分析工具，基于`.NET`，用来简要的分析IIS的日志。

## ShutDown
定时关机小程序，基于`.NET`，有两个版本。

## OfficeTool
验证码识别、自动打卡工具，基于`.NET`，用来识别简单的验证码，并模拟网页操作，进行上下班打卡。

## ScheduledTask
Window计划任务服务，基于`.NET`，用来定时执行dll中的方法，易于扩展。