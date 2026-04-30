// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月30日 08:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day11\Services\ContactService.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Day11.Factories;
using Day11.Models;
using Day11.Strategies;
using Day11.Utils;

namespace Day11.Services;

public class ContactService(ISaveStrategy saveStrategy)
{
    private readonly List<Contact> _contacts = [];

    public void AddContact(string name, string phone)
    {
        var contact = ContactFactory.CreateContact(name, phone);
        _contacts.Add(contact);
    }

    public List<Contact> GetAllContacts()
    {
        return _contacts.ToList();
    }

    public Contact? GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;
        var contact = _contacts.Find(c => c.Name == name);
        return contact;
    }

    public async Task SaveToFileAsync()
    {
        await saveStrategy.SaveAsync(AppConfig.DataFilePath, _contacts);
    }

    public async Task LoadFromFileAsync()
    {
        var data = await saveStrategy.LoadAsync(AppConfig.DataFilePath);
        _contacts.Clear();
        _contacts.AddRange(data);
    }
}