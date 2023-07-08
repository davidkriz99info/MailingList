using System.ComponentModel;
using MailingListApi.Models;
using MailingListApi.Requests;

namespace MailingListApi.DAL
{
    public interface IMailingListRepository
    {
        IEnumerable<Entry> GetEntries(string lastName = "", ListSortDirection nameSortDirection = ListSortDirection.Ascending);

        Entry? GetEntryByEmailAddress(string emailAddress);

        void InsertEntry(EntryRequest entryRequest);
    }
}
