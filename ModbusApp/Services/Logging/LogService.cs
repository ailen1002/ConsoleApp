// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月09日 14:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Services\Logging\LogService.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Serilog;
using Serilog.Context;

namespace ModbusApp.Services.Logging;

public class LoggingService(ILogger logger) : ILogService
{
    public void Information(string message, params object[] args)
    {
        logger.Information(message, args);
    }

    public void Warning(string message, params object[] args)
    {
        logger.Warning(message, args);
    }

    public void Error(string message, params object[] args)
    {
        logger.Error(message, args);
    }

    public void Error(Exception exception, string message, params object[] args)
    {
        logger.Error(exception, message, args);
    }

    public void Debug(string message, params object[] args)
    {
        logger.Debug(message, args);
    }

    public void Verbose(string message, params object[] args)
    {
        logger.Verbose(message, args);
    }

    public IDisposable PushProperty(string name, object value)
    {
        return LogContext.PushProperty(name, value);
    }
}