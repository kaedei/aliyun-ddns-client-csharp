# aliyun-ddns-client-csharp
基于阿里云解析服务API的DDNS客户端。将本机IP更新至指定域名的DNS A记录，配合定时任务可以达到花生壳的效果。

# 使用方法
1. 在阿里云申请一个域名，将此域名添加一个子域（如`www`），并设置为A类型记录，IP地址随便填写一个（程序会自动修改）
2. 到阿里云域名控制台[申请AccessId Key和Secrect](https://ak-console.aliyun.com/#/accesskey)
3. Clone本项目代码到本机，使用VS2013或更高版本编译
4. 将程序exe和其他dll文件复制到服务器上。在exe文件同目录下创建一个文本文件并命名为`config.txt`
5. `config.txt`文件的内容有四行：
  1. 第一行：Access Id Key，例如 *DR2DPjKmg4ww0e79*
  2. 第二行：Access Id Secret，例如 *ysHnd1dhWvoOmbdWKx04evlVEdXEW7*
  3. 第三行：域名，例如 *google.com*
  4. 第四行：子域名，例如 *www*
6. 在服务器上运行主exe即可

# 环境 
使用VS2013 + .NET 4.5 + C#开发

# 建议
建议通过任务计划定时调用（如每两小时），程序会判断是否需要修改A记录
