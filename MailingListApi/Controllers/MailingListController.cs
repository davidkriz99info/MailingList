using MailingListApi.DAL;
using MailingListApi.Models;
using MailingListApi.Requests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace MailingListApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MailingListController : ControllerBase
{
    IMailingListRepository _repository;

    public MailingListController(IMailingListRepository mailingListRepository)
    {
        _repository = mailingListRepository;
    }

    [HttpPost("AddEntry")]
    public ActionResult AddEntry(EntryRequest entryRequest)
    {
        if (!IsValidEmailAddress(entryRequest.EmailAddress))
        {
            return BadRequest(new { errors = new { EmailAddress = new [] { "Invalid email address." }}});
        }

        var existingEmailAddress = _repository.GetEntryByEmailAddress(entryRequest.EmailAddress);

        if (existingEmailAddress != null)
        {
            return BadRequest(new { errors = new { EmailAddress = new [] { "An entry with this email address already exists." }}});
        }

        _repository.InsertEntry(entryRequest);

        return Ok();
    }

    [HttpGet("GetEntries")]
    public IEnumerable<Entry> GetEntries(string lastName = "", string nameSortOrder = "") 
    {
        var nameSortDirection = nameSortOrder.Contains("desc", StringComparison.InvariantCultureIgnoreCase)
            ? ListSortDirection.Descending
            : ListSortDirection.Ascending;

        return _repository.GetEntries(lastName, nameSortDirection);
    }

    private static bool IsValidEmailAddress(string emailAddress)
    {
        if (string.IsNullOrEmpty(emailAddress))
        {
            return false;
        }

        var trimmedEmailAddress = emailAddress.Trim();

        if (trimmedEmailAddress.EndsWith("."))
        {
            return false;
        } 
        
        try
        {
            var mailAddress = new System.Net.Mail.MailAddress(emailAddress);

            return mailAddress.Address == trimmedEmailAddress;
        } 
        catch 
        {
            return false;
        }
    }
}
