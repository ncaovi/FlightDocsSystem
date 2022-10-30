using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Service;
using FlightDocsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using FlightDocsSystem.Interface;
using FlightDocsSystem.Model;

namespace FlightDocsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class DocumentListController : ControllerBase
    {
        private readonly IDocumentList _document;
        private readonly DataContext _context;
        public DocumentListController(DataContext context, IDocumentList document)
        {
            _context = context;
            _document = document;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddDocument(DocumentList document)
        {
            try
            {
                await _document.AddDocumentListAsync(document);
            }
            catch (Exception ex)
            {

            }
            return Ok(new
            {
                retCode = 1,
                retText = "Thêm thành công"
            });
        }
        [HttpGet]
        [Route("ListDocument")]
        public async Task<ActionResult<IEnumerable<DocumentList>>> GetDocumentAllAsync()
        {
            return await _document.GetDocumentListAllAsync();

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, DocumentList document)
        {
            if (id != document.DocumentId)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _document.EditDocumentListAsync(id, document);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(
                 new
                 {
                     retCode = 1,
                     retText = "Sửa thành công"
                 });
        }
        private bool DocumentExists(int id)
        {
            return _context.GroupPermissions.Any(e => e.GroupId == id);

        }
        [HttpDelete("{id}")]


        public async Task<IActionResult> DeleteDocument(int id)
        {
            var Document = await _context.DocumentLists.FindAsync(id);
            if (Document == null)
            {
                return NotFound();
            }

            _context.DocumentLists.Remove(Document);
            await _context.SaveChangesAsync();

            return Ok(
                 new
                 {
                     retCode = 1,
                     retText = "Xóa thành công"
                 });
        }
    }
}