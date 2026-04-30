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

namespace Day11.Services;

public class ContactService(FileStorageService fileService)
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
        var lines = _contacts.Select(c => $"{c.Name}|{c.Phone}").ToList();
        await fileService.SaveAllLineAsync(lines);
    }

    public async Task LoadFromFileAsync()
    {
        _contacts.Clear();
        var lines = await fileService.LoadAllLinesAsync();

        foreach (var arr in lines.Select(line => line.Split('|')).Where(arr => arr.Length > 2))
        {
            _contacts.Add(new Contact
            {
                Name = arr[0],
                Phone = arr[1]
            });
        }
    }
}