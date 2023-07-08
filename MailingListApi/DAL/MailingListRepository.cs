using System.ComponentModel;
using MailingListApi.Models;
using MailingListApi.Requests;

namespace MailingListApi.DAL
{
    public class MailingListRepository : IMailingListRepository
    {
        // TODO replace with context or other datastore functionality
        private readonly List<Entry> entries;

        public MailingListRepository()
        {
            entries = new List<Entry>();
        }

        public IEnumerable<Entry> GetEntries(string lastName = "", ListSortDirection nameSortDirection = ListSortDirection.Ascending)
        {
            var filteredEntries = entries.Where(e => string.IsNullOrEmpty(lastName) || e.LastName == lastName);

            var sortedEntries = nameSortDirection == ListSortDirection.Ascending
                ? filteredEntries.OrderBy(e => e.LastName + e.FirstName)
                : filteredEntries.OrderByDescending(e => e.LastName + e.FirstName);

            return sortedEntries;
        }

        public Entry? GetEntryByEmailAddress(string emailAddress)
        {
            return entries.FirstOrDefault(e => e?.EmailAddress == emailAddress, null);
        }

        public void InsertEntry(EntryRequest entryRequest)
        {
            var entry = new Entry
            {
                Id = entries.Count + 1,
                Guid = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                EmailAddress = entryRequest.EmailAddress,
                FirstName = entryRequest.FirstName,
                LastName = entryRequest.LastName
            };

            entries.Add(entry);
        }
    }
}
