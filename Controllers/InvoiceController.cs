using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BVPortalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize(Roles = "ADMIN")]
    public class InvoiceController : ControllerBase
    {
        private const string cacheKey = "invoiceList";
        private readonly BVContext DBContext;
        private IMemoryCache _cache;
        private ILogger<InvoiceController> _logger;
        private readonly IMapper _mapper; 

        public InvoiceController(BVContext DBContext, IMemoryCache cache, ILogger<InvoiceController> logger, IMapper mapper)
        {
            this.DBContext = DBContext;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper; 
        }

        [HttpGet("GetNextInvoiceNumber")]
        public ActionResult<int> GetNextInvoiceNumber()
        {
            return DBContext.Invoice.Max(x=>x.InvoiceNumber);
        }

        [HttpGet("GetInvoice")]
        public async Task<ActionResult<List<InvoiceDTO>>> Get()
        {
            // var List = await DBContext.Invoice.Select(
            //     s => new InvoiceDTO
            //     {
            //         Id = s.Id,
            //         InvoiceNumber = s.InvoiceNumber,
            //         InvoiceDate = s.InvoiceDate,
            //         Term = s.Term,
            //         DueDate = s.DueDate,
            //         CompanyId = s.CompanyId,
            //         CompanyName = s.CompanyName,
            //         CompanyAddressLine1 = s.CompanyAddressLine1,
            //         CompanyAddressLine2 = s.CompanyAddressLine2,
            //         CompanyAddressLine3 = s.CompanyAddressLine3,
            //         CompanyPhoneNumber = s.CompanyPhoneNumber,
            //         CompanyEmailAddress = s.CompanyEmailAddress,
            //         CustomerId = s.CustomerId,
            //         CustomerName = s.CustomerName,
            //         CustomerAddressLine1 = s.CustomerAddressLine1,
            //         CustomerAddressLine2 = s.CustomerAddressLine2,
            //         CustomerAddressLine3 = s.CustomerAddressLine3,
            //         Status = s.Status,
            //         NoteToCustomer = s.NoteToCustomer,
            //         GetPaidNotes = s.GetPaidNotes,
            //         Total = s.InvoiceProduct.Select(x=>x.Total).Sum(),
            //         Products =  (DBContext.InvoiceProduct.Where(x=>x.InvoiceId == s.Id).Select(
            //         s => new InvoiceProductDTO
            //         {
            //             Id = s.Id,
            //             InvoiceId = s.InvoiceId,
            //             ItemTypeId = s.ItemTypeId,
            //             Unit = s.Unit,
            //             Rate = s.Rate,
            //             Quantity = s.Quantity,
            //             Total = s.Total,
            //             Product = s.Product,
            //             Service = s.Service,
            //             IsProduct = s.IsProduct
            //         }).ToList() )               
            //     }
            // ).OrderByDescending(x=>x.InvoiceNumber).ToListAsync();
            _logger.Log(LogLevel.Information, "Trying to fetch the list of Invoices from cache.");
            if (_cache.TryGetValue(cacheKey, out List<InvoiceDTO> List))
            {
                _logger.Log(LogLevel.Information, "Invoice list found in cache.");
                
            }
            else
            {
                _logger.Log(LogLevel.Information, "Invoice list not found in cache. Fetching from database.");
                List = _mapper.Map<List<InvoiceDTO>>(await DBContext.Invoice.ToListAsync());
                
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set(cacheKey, List, cacheEntryOptions);
            }
            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetInvoiceById/{id}")]
        public async Task<ActionResult<InvoiceDTO>> GetInvoiceById(int id)
        {
            var List = await DBContext.Invoice.Where(x=>x.Id == id).Select(
                s => new InvoiceDTO
                {
                    Id = s.Id,
                    InvoiceNumber = s.InvoiceNumber,
                    InvoiceDate = s.InvoiceDate,
                    Term = s.Term,
                    DueDate = s.DueDate,
                    CompanyId = s.CompanyId,
                    CompanyName = s.CompanyName,
                    CompanyAddressLine1 = s.CompanyAddressLine1,
                    CompanyAddressLine2 = s.CompanyAddressLine2,
                    CompanyAddressLine3 = s.CompanyAddressLine3,
                    CompanyPhoneNumber = s.CompanyPhoneNumber,
                    CompanyEmailAddress = s.CompanyEmailAddress,
                    CustomerId = s.CustomerId,
                    CustomerName = s.CustomerName,
                    CustomerAddressLine1 = s.CustomerAddressLine1,
                    CustomerAddressLine2 = s.CustomerAddressLine2,
                    CustomerAddressLine3 = s.CustomerAddressLine3,
                    Status = s.Status,
                    NoteToCustomer = s.NoteToCustomer,
                    GetPaidNotes = s.GetPaidNotes
                }
            ).FirstOrDefaultAsync();
            
            if (List==null)
            {
                return NotFound();
            }
            else
            {
                List.Products = DBContext.InvoiceProduct.Where(x=>x.InvoiceId == id).Select(
                s => new InvoiceProductDTO
                {
                    Id = s.Id,
                    InvoiceId = s.InvoiceId,
                    ItemTypeId = s.ItemTypeId,
                    Unit = s.Unit,
                    Rate = s.Rate,
                    Quantity = s.Quantity,
                    Total = s.Total,
                    Product = s.Product,
                    Service = s.Service,
                    IsProduct = s.IsProduct
                }).ToList();
                return List;
            }
        }

        [HttpPost("InsertInvoice")]
        public async Task < HttpStatusCode > InsertInvoice(InvoiceDTO s) {
            // var entity = new Invoice() {
            //         Id = s.Id,
            //         InvoiceNumber = s.InvoiceNumber,
            //         InvoiceDate = s.InvoiceDate,
            //         Term = s.Term,
            //         DueDate = s.DueDate,
            //         CompanyId = s.CompanyId,
            //         CompanyName = s.CompanyName,
            //         CompanyAddressLine1 = s.CompanyAddressLine1,
            //         CompanyAddressLine2 = s.CompanyAddressLine2,
            //         CompanyAddressLine3 = s.CompanyAddressLine3,
            //         CompanyPhoneNumber = s.CompanyPhoneNumber,
            //         CompanyEmailAddress = s.CompanyEmailAddress,
            //         CustomerId = s.CustomerId,
            //         CustomerName = s.CustomerName,
            //         CustomerAddressLine1 = s.CustomerAddressLine1,
            //         CustomerAddressLine2 = s.CustomerAddressLine2,
            //         CustomerAddressLine3 = s.CustomerAddressLine3,
            //         Status = "NEW",
            //         NoteToCustomer = s.NoteToCustomer,
            //         GetPaidNotes = s.GetPaidNotes
            // };
            var entity = _mapper.Map<Invoice>(s);
            DBContext.Invoice.Add(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            List<InvoiceProduct> p = s.Products.Select(
                s => new InvoiceProduct
                {
                    Product = s.IsProduct?s.Name:"",
                    Service = s.IsProduct?"":s.Name,
                    InvoiceId = entity.Id,
                    ItemTypeId = s.ItemTypeId,
                    Unit = s.Unit,
                    Rate = s.Rate,
                    Quantity = s.Quantity,
                    Total = s.Total,
                    IsProduct = s.IsProduct
                }
            ).ToList();
            DBContext.InvoiceProduct.AddRange(p);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.Created;
        }
        [HttpPut("UpdateInvoice")]
        public async Task<HttpStatusCode> UpdateInvoice(InvoiceDTO s) {
            var entity = await DBContext.Invoice.FirstOrDefaultAsync(x => x.Id == s.Id);
            entity.Id = s.Id;
            entity.InvoiceNumber = s.InvoiceNumber;
            entity.InvoiceDate = s.InvoiceDate;
            entity.Term = s.Term;
            entity.DueDate = s.DueDate;
            entity.CompanyId = s.CompanyId;
            entity.CompanyName = s.CompanyName;
            entity.CompanyAddressLine1 = s.CompanyAddressLine1;
            entity.CompanyAddressLine2 = s.CompanyAddressLine2;
            entity.CompanyAddressLine3 = s.CompanyAddressLine3;
            entity.CompanyPhoneNumber = s.CompanyPhoneNumber;
            entity.CompanyEmailAddress = s.CompanyEmailAddress;
            entity.CustomerId = s.CustomerId;
            entity.CustomerName = s.CustomerName;
            entity.CustomerAddressLine1 = s.CustomerAddressLine1;
            entity.CustomerAddressLine2 = s.CustomerAddressLine2;
            entity.CustomerAddressLine3 = s.CustomerAddressLine3;
            entity.Status = s.Status;
            entity.NoteToCustomer = s.NoteToCustomer;
            entity.GetPaidNotes = s.GetPaidNotes;

            await DBContext.SaveChangesAsync();
            IQueryable<InvoiceProduct> ip = DBContext.InvoiceProduct.Where(x=>x.InvoiceId ==s.Id);
            DBContext.InvoiceProduct.RemoveRange(ip);
            List<InvoiceProduct> p = s.Products.Select(
                s => new InvoiceProduct
                {
                    Product = s.IsProduct?s.Name:"",
                    Service = s.IsProduct?"":s.Name,
                    InvoiceId = entity.Id,
                    ItemTypeId = s.ItemTypeId,
                    Unit = s.Unit,
                    Rate = s.Rate,
                    Quantity = s.Quantity,
                    Total = s.Total,
                    IsProduct = s.IsProduct
                }
            ).ToList();
            DBContext.InvoiceProduct.AddRange(p);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteInvoice/{Id}")]
        public async Task < HttpStatusCode > DeleteInvoice(int Id) {
             List<InvoiceProduct> products = DBContext.InvoiceProduct.Where(x=>x.InvoiceId == Id)
             .Select(i => new InvoiceProduct(){
                Id = i.Id
            }).ToList();
            DBContext.InvoiceProduct.AttachRange(products);
            DBContext.InvoiceProduct.RemoveRange(products);

            var entity = new Invoice() {
                Id = Id
            };
            DBContext.Invoice.Attach(entity);
            DBContext.Invoice.Remove(entity);
            await DBContext.SaveChangesAsync();
            _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
        [HttpPost("DeleteInvoices")]
        public  async Task<HttpStatusCode> DeleteInvoices(List<InvoiceDTO> invoices) {
        List<Invoice> entities = invoices.Select(i => new Invoice(){
                Id = i.Id
            }).ToList();
            DBContext.Invoice.AttachRange(entities);
            DBContext.Invoice.RemoveRange(entities);
            await DBContext.SaveChangesAsync();
             _cache.Remove(cacheKey);
            return HttpStatusCode.OK;
        }
    }
}