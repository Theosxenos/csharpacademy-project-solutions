using Phonebook.Models;
using Phonebook.Repositories;
using Phonebook.Views;

namespace Phonebook.Controllers;

public class PhoneBookController
{
    private PhoneBookView view = new();
    private PhoneBookRepository repository = new();
    
    public void AddContact()
    {
        var retry = false;
        do
        {
            var contactName = view.GetContactName();
            var email = view.GetContactEmail();
            var phoneNumber = view.GetPhoneNumber();

            try
            {
                repository.AddContact(new Contact { Name = contactName, Email = email, PhoneNumber = phoneNumber });
                retry = false;
            }
            catch (ArgumentException e)
            {
                view.ShowError(e.Message);
                retry = view.AskConfirm("Retry?");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        } while (retry);
    }

    public void UpdateContact()
    {
        
        var retry = false;
        do
        {
            var contacts = repository.GetAll();
            if (contacts.Count == 0)
            {
                view.ShowError("No contacts found.");
                continue;
            }
            
            var contact = view.ShowMenu(contacts);
            
            var contactName = view.GetContactName(contact.Name);
            var email = view.GetContactEmail(contact.Email);
            var phoneNumber = view.GetPhoneNumber(contact.PhoneNumber);

            try
            {
                repository.UpdateContact(new Contact{Id = contact.Id, Name = contactName, Email = email, PhoneNumber = phoneNumber});
                retry = false;
            }
            catch (ArgumentException e)
            {
                view.ShowError(e.Message);
                retry = view.AskConfirm("Retry?");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        } while (retry);
    }

    public void ListContacts()
    {
        var contacts = repository.GetAll();
        if (contacts.Count == 0)
        {
            view.ShowError("No contacts found.");
            return;
        }

        view.ShowTable(contacts);
    }

    public void DeleteContact()
    {
        var contacts = repository.GetAll();
        if (contacts.Count == 0)
        {
            view.ShowError("No contacts found.");
            return;
        }

        var contact = view.ShowMenu(contacts);
        repository.DeleteContact(contact);
    }
}