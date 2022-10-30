using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Interface;
using FlightDocsSystem.Model;
using FlightDocsSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace FlightDocsSystem.Service
{
    public class DocumentListSvc : IDocumentList
    {
        protected DataContext _context;
        public DocumentListSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddDocumentListAsync(DocumentList DocumentLists)
        {
            _context.Add(DocumentLists);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditDocumentListAsync(int id, DocumentList DocumentLists)
        {
            _context.DocumentLists.Update(DocumentLists);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DocumentList>> GetDocumentListAllAsync()
        {
            var dataContext = _context.DocumentLists;
            return await dataContext.ToListAsync();
        }

        public async Task<DocumentList> GetDocumentListAsync(int? id)
        {
            var DocumentLists = await _context.DocumentLists
                .FirstOrDefaultAsync(m => m.DocumentId == id);
            if (DocumentLists == null)
            {
                return null;
            }

            return DocumentLists;
        }

    }
}