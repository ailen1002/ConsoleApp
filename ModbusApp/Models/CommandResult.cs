// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月16日 10:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Models\CommandResult.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace ModbusApp.Models;

public struct CommandResult
{
    public string CommandName { get; set; }
    public bool Success { get; set; }
    public byte[] Response { get; set; }
    public int ActualLength { get; set; }
}