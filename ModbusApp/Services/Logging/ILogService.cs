// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月09日 14:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Services\Logging\ILogService.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace ModbusApp.Services.Logging;

public interface ILogService
{
    void Information(string message, params object[] args);
    void Warning(string message, params object[] args);
    void Error(string message, params object[] args);
    void Error(Exception exception, string message, params object[] args);
    void Debug(string message, params object[] args);
    void Verbose(string message, params object[] args);
    IDisposable PushProperty(string name, object value);
}