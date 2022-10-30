using FlightDocsSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightDocsSystem.Interface
{
    public interface IDocumentList
    {
        public Task<List<DocumentList>> GetDocumentListAllAsync();
        public Task<bool> EditDocumentListAsync(int id, DocumentList DocumentLists);
        public Task<bool> AddDocumentListAsync(DocumentList DocumentLists);
        public Task<DocumentList> GetDocumentListAsync(int? id);
    }
}
